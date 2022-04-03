namespace UseCases.Common;

/// <summary>
/// Upload response.
/// </summary>
public record Response<T>
{
    /// <summary>
    /// Result of response.
    /// </summary>
    public T Result { get; init; }
}
