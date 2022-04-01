using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Infrastructure.Abstractions;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure;

/// <summary>
/// AWS S3 file storage.
/// </summary>
public class S3FileStorage : IFileStorage
{
    private readonly S3Credentials credentials;

    public S3FileStorage(IOptions<S3Credentials> credentialOptions)
    {
        this.credentials = credentialOptions.Value;
        //transferUtility = new TransferUtility(s3Client);
    }

    private static List<Tag> GetFileTags(bool isDeleting)
    {
        return new List<Tag>{
                    new Tag { Key = S3ObjectTags.Delete, Value = isDeleting.ToString()}
                };
    }

    public async Task<GetObjectResponse> Download(BucketFile model)
    {
        var tags = await GetObjectTags(model.BucketName, model.FileKey);

        var response = await DownloadFileAsync(model);

        if (deletionFlag)
            DeleteFileAsync(model);
        return response;
    }


    private static string GetTempFilePath(string text)
    {
        var path = Path.GetTempFileName();
        Path.ChangeExtension(path, ".txt");
        File.WriteAllText(path, text);
        return path;
    }

    private async Task<GetObjectResponse> DownloadFileAsync(BucketFile downloadModel)
    {
        var response = await transferUtility.S3Client.GetObjectAsync(new GetObjectRequest()
        {
            BucketName = downloadModel.BucketName,
            Key = downloadModel.FileKey
        });
        return response;
    }

    public async Task<IEnumerable<Tag>> GetObjectTags(string bucketName, string fileKey)
    {
        var getTagsRequest = new GetObjectTaggingRequest
        {
            BucketName = bucketName,
            Key = fileKey
        };

        return (await s3Client.GetObjectTaggingAsync(getTagsRequest)).Tagging;

    }


    public async Task CreateBucketAsync(string bucketName)
    {
        await s3Client.PutBucketAsync(new PutBucketRequest() { BucketName = bucketName, UseClientRegion = true });
    }

    public async Task<IEnumerable<S3Object>> GetBucketObjects(string bucketName)
    {
        return (await s3Client.ListObjectsV2Async(new ListObjectsV2Request() { BucketName = bucketName })).S3Objects;
    }

    public async Task<Dictionary<string, bool>> GetObjectKeysWithDeletionTag(string bucketName)
    {
        var dict = new Dictionary<string, bool>();

        foreach (var e in await GetBucketObjects(bucketName))
        {
            dict.Add(e.Key, Convert.ToBoolean((await GetObjectTags(bucketName, e.Key)).First(x => x.Key == S3ObjectTags.Delete).Value));
        }

        return dict;
    }

    public Task DownloadAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UplaodAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        using var s3Client = GetS3Client();
        using var transferUtility = new TransferUtility(s3Client);


        await file.CopyToAsync(memoryStream, cancellationToken);
        await transferUtility.UploadAsync(new TransferUtilityUploadRequest()
        {
            AutoCloseStream = true,
            InputStream = memoryStream,
            //TagSet = GetFileTags(uploadModel.IsDeleting),
            //BucketName = uploadModel.BucketName,
            //Key = uploadModel.File.FileName
        });
        
    }

    public async Task UploadTextAsync(string text, CancellationToken cancellationToken)
    {
        using var s3Client = GetS3Client();
        using var transferUtility = new TransferUtility(s3Client);
        var path = GetTempFilePath(text);

        using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
        await transferUtility.UploadAsync(new TransferUtilityUploadRequest()
        {
            AutoCloseStream = true,
            InputStream = file,
            //TagSet = GetFileTags(model.IsDeleting),
            //BucketName = model.BucketName,
            Key = Guid.NewGuid().ToString() + ".txt"
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteFileAsync(int id, CancellationToken cancellationToken)
    {
        using var s3Client = GetS3Client();
        var response = await s3Client.DeleteObjectAsync(
            new DeleteObjectRequest
            {
                BucketName = "SomeBucket",
                Key = "SomeKey"
            }, cancellationToken);
        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new HttpRequestException("Deletion failed.", inner: null, response.HttpStatusCode);
        }
    }

    private AmazonS3Client GetS3Client()
    {
        var awsCredentials =
            new BasicAWSCredentials(credentials.AccessKeyId, credentials.SecretAccessKeyId);
        var s3client = new AmazonS3Client(awsCredentials, RegionEndpoint.EUNorth1);
        return s3client;
    }
}
