using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webnangcao.Entities;
using webnangcao.Entities.Joins;

namespace webnangcao.Context;

public class ApplicationContext : IdentityDbContext<AppUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    
    public DbSet<Track> CacTrack { get; set; } = null!;
    public DbSet<Playlist> Playlists { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Playlist_Tag> Playlist_Tag { get; set; } = null!;
    public DbSet<Track_Playlist> Track_Playlist { get; set; } = null!;
    public DbSet<Track_Category> Track_Category { get; set; } = null!;
    public DbSet<Follow> Follows { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var type in builder.Model.GetEntityTypes())
        {
            var tableName = type.GetTableName()!;
            if (tableName.StartsWith("AspNet"))
            {
                type.SetTableName(tableName.Replace("AspNet", ""));
            }
        }
    }
}