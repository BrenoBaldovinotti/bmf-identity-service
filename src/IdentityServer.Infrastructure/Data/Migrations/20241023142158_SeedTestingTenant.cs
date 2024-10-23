using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestingTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed a new tenant with a UUID and hashed API key
            var tenantId = Guid.NewGuid();
            var apiKey = "964ae197-922c-48eb-a912-f9a41665666f:3LxoEtOPofPsFbuWHKoa7w=="; // Only for testing purposes
            var hashedApiKey = "6OufyYwaSQ+Lda+ubAE9tJFa1Tof3fjUy2vjgiB+UYE="; // Replace with hashed version using your helper logic

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "ApiKeyHash", "CreatedAt" },
                values: new object[] { tenantId, "Testing Tenant", hashedApiKey, DateTime.UtcNow }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the testing tenant if needed
            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("your-tenant-guid-here") // Use the same tenant ID from the 'Up' method
            );
        }
    }
}
