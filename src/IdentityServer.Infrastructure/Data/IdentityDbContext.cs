using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Ensure Application Key is unique
        builder.Entity<Application>()
            .HasIndex(a => a.Key)
            .IsUnique();

        builder.Entity<ApplicationUser>()
            .HasKey(au => new { au.UserId, au.ApplicationId });

        builder.Entity<ApplicationUser>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(au => au.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUser>()
            .HasOne<Application>()
            .WithMany()
            .HasForeignKey(au => au.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
