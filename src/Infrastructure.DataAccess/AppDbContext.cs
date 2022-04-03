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
    /// <inheritdoc/>
    public DbSet<StoredFile> StoredFiles { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Options.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        RestrictCascadeDelete(builder);

        builder.Entity<StoredFile>()
            .HasQueryFilter(file => file.DeletedAt == null);
    }

    private static void RestrictCascadeDelete(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
