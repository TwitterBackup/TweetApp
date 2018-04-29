using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class RemovedIdeletabeIAuditableFromMainClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "SavedOn",
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
                name: "ModifiedOn",
                table: "Tweeters");

            migrationBuilder.DropColumn(
                name: "SavedOn",
                table: "Tweeters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ModifiedOn",
                table: "Tweets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SavedOn",
                table: "Tweets",
                nullable: true);

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
                name: "ModifiedOn",
                table: "Tweeters",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SavedOn",
                table: "Tweeters",
                nullable: true);
        }
    }
}
