using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameApplicationToTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Applications_ApplicationId",
                table: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "ApplicationUsers",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_ApplicationId",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_TenantId");

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Key",
                table: "Tenants",
                column: "Key",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Tenants_TenantId",
                table: "ApplicationUsers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Tenants_TenantId",
                table: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "ApplicationUsers",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_TenantId",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_ApplicationId");

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Key",
                table: "Applications",
                column: "Key",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Applications_ApplicationId",
                table: "ApplicationUsers",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
