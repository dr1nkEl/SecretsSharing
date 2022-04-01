using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace Domain;

/// <summary>
/// Application user.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// Stored files of user.
    /// </summary>
    public ICollection<StoredFile> StoredFiles { get; set; } = new Collection<StoredFile>();
}
