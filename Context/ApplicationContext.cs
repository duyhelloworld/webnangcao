using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webnangcao.Entities;
using webnangcao.Entities.Joins;

namespace webnangcao.Context;

public class ApplicationContext : IdentityDbContext<User, Role, string>
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

    // Bảng liên kết n-n
    public DbSet<Track_Playlist> Track_Playlists { get; set; } = null!;
    public DbSet<Track_Category> Track_Categories { get; set; } = null!;

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