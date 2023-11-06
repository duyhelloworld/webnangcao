using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webnangcao.Entities;
using webnangcao.Entities.Joins;

namespace webnangcao.Context;

public class ApplicationContext : IdentityDbContext<User, Role, long>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    
    // Thực thể thuần
    public DbSet<Track> Tracks { get; set; } = null!;
    public DbSet<Playlist> Playlists { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    // Bảng quan hệ
    public DbSet<Follow> Follows { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<UserTrackAction> UserTrackActions { get; set; } = null!;
    public DbSet<UserPlaylistAction> UserPlaylistActions { get; set; } = null!;

    // Bảng liên kết n-n
    public DbSet<TrackPlaylist> TrackPlaylists { get; set; } = null!;
    public DbSet<TrackCategory> TrackCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
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