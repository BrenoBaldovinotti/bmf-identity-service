using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameApplicationUsersToTenantUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_UserId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Tenants_TenantId",
                table: "ApplicationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers");

            migrationBuilder.RenameTable(
                name: "ApplicationUsers",
                newName: "TenantUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_TenantId",
                table: "TenantUsers",
                newName: "IX_TenantUsers_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantUsers",
                table: "TenantUsers",
                columns: new[] { "UserId", "TenantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_AspNetUsers_UserId",
                table: "TenantUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_Tenants_TenantId",
                table: "TenantUsers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_AspNetUsers_UserId",
                table: "TenantUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_Tenants_TenantId",
                table: "TenantUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantUsers",
                table: "TenantUsers");

            migrationBuilder.RenameTable(
                name: "TenantUsers",
                newName: "ApplicationUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TenantUsers_TenantId",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers",
                columns: new[] { "UserId", "TenantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_UserId",
                table: "ApplicationUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Tenants_TenantId",
                table: "ApplicationUsers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
