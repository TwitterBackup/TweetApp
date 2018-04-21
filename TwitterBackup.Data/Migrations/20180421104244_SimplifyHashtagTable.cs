using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class SimplifyHashtagTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "SavedOn",
                table: "Hashtags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Hashtags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Hashtags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Hashtags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Hashtags",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SavedOn",
                table: "Hashtags",
                nullable: true);
        }
    }
}
