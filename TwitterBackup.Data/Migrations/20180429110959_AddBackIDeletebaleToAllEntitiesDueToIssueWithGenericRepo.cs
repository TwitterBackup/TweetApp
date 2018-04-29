using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class AddBackIDeletebaleToAllEntitiesDueToIssueWithGenericRepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Tweets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tweets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "TweetHashtags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TweetHashtags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Tweeters",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tweeters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Hashtags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Hashtags",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "TweetHashtags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TweetHashtags");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Tweeters");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tweeters");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Hashtags");
        }
    }
}
