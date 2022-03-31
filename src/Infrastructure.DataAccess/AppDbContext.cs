using Domain;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

/// <summary>
/// Application database context.
/// </summary>
public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IAppDbContext
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Options.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
