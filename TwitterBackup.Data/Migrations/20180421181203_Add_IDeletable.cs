using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class Add_IDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserTweets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserTweets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserTweeters",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserTweeters",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserTweets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserTweets");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserTweeters");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserTweeters");
        }
    }
}
