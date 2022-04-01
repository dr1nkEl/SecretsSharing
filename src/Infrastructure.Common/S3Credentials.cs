namespace Infrastructure.Common;

/// <summary>
/// Credentials for AWS S3 storage.
/// </summary>
public record S3Credentials
{
    /// <summary>
    /// Access key ID.
    /// </summary>
    public string AccessKeyId { get; set; }

    /// <summary>
    /// Secret access key ID.
    /// </summary>
    public string SecretAccessKeyId { get; set; }
}
