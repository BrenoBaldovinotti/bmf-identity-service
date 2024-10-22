using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Data.Migrations;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantUser> TenantUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Ensure Application Key is unique
        builder.Entity<Tenant>()
            .HasIndex(t => t.Key)
            .IsUnique();

        builder.Entity<TenantUser>()
            .HasKey(au => new { au.UserId, au.TenantId });

        builder.Entity<TenantUser>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(tu => tu.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TenantUser>()
            .HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(tu => tu.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
