using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class SavedOn_ModifiedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserTweets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SavedOn",
                table: "UserTweets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserTweeters",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SavedOn",
                table: "UserTweeters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserTweets");

            migrationBuilder.DropColumn(
                name: "SavedOn",
                table: "UserTweets");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserTweeters");

            migrationBuilder.DropColumn(
                name: "SavedOn",
                table: "UserTweeters");
        }
    }
}
