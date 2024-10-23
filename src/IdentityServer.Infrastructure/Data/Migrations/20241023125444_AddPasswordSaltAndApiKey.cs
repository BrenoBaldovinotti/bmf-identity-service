using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordSaltAndApiKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Tenants",
                newName: "ApiKeyHash");

            migrationBuilder.RenameIndex(
                name: "IX_Tenants_Key",
                table: "Tenants",
                newName: "IX_Tenants_ApiKeyHash");

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ApiKeyHash",
                table: "Tenants",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_Tenants_ApiKeyHash",
                table: "Tenants",
                newName: "IX_Tenants_Key");
        }
    }
}
