using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotLights.Data.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class IsShowInHomePagecolumnaddedtoCategorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Storages");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Storages",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadAt",
                table: "Storages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsShowInHomePage",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadAt",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "IsShowInHomePage",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Storages",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Storages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Storages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
