using Infrastructure.Abstractions;

namespace Infrastructure;

//PSEUDO CODE
public class S3FileStorage : IFileStorage
{
    private readonly AmazonS3Client s3Client;
    private readonly TransferUtility transferUtility;

    public S3Service(AmazonS3Client s3Client)
    {
        this.s3Client = s3Client;
        transferUtility = new TransferUtility(s3Client);
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

        bool deletionFlag = Convert.ToBoolean(tags.First(x => x.Key == S3ObjectTags.Delete).Value);

        if (deletionFlag)
            DeleteFileAsync(model);
        return response;
    }

    public async Task UploadFileAsync(BucketFile uploadModel)
    {
        using (var memoryStream = new MemoryStream())
        {
            await uploadModel.File.CopyToAsync(memoryStream);
            await transferUtility.UploadAsync(new TransferUtilityUploadRequest()
            {
                AutoCloseStream = true,
                InputStream = memoryStream,
                TagSet = GetFileTags(uploadModel.IsDeleting),
                BucketName = uploadModel.BucketName,
                Key = uploadModel.File.FileName
            });
        }
    }

    public async Task UploadTextAsync(BucketFile model)
    {
        var path = GetTempFilePath(model.TextContent);

        using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            await transferUtility.UploadAsync(new TransferUtilityUploadRequest()
            {
                AutoCloseStream = true,
                InputStream = file,
                TagSet = GetFileTags(model.IsDeleting),
                BucketName = model.BucketName,
                Key = Guid.NewGuid().ToString() + ".txt"
            });
        }
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

    public async void DeleteFileAsync(BucketFile deleteModel)
    {
        await s3Client.DeleteObjectAsync(new DeleteObjectRequest() { BucketName = deleteModel.BucketName, Key = deleteModel.FileKey });
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
    
}
