using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscription.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "FullName" },
                values: new object[] { new Guid("8d868b2a-df36-4a0e-bf40-7271c4f014f7"), new DateTime(2024, 4, 9, 12, 0, 0, 0, DateTimeKind.Utc), "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8d868b2a-df36-4a0e-bf40-7271c4f014f7"));
        }
    }
}
