using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class Added_Validations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Tweeters_TweeterId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Favorited",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Retweeted",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "FavouritesCount",
                table: "Tweeters");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Tweets",
                newName: "SavedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Tweeters",
                newName: "SavedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Hashtags",
                newName: "SavedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "AspNetUsers",
                newName: "SavedOn");

            migrationBuilder.AlterColumn<string>(
                name: "TweeterId",
                table: "Tweets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Tweets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Tweets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ScreenName",
                table: "Tweeters",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Hashtags",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Tweeters_TweeterId",
                table: "Tweets",
                column: "TweeterId",
                principalTable: "Tweeters",
                principalColumn: "TweeterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Tweeters_TweeterId",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "SavedOn",
                table: "Tweets",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "SavedOn",
                table: "Tweeters",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "SavedOn",
                table: "Hashtags",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "SavedOn",
                table: "AspNetUsers",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<string>(
                name: "TweeterId",
                table: "Tweets",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Tweets",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Tweets",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "Favorited",
                table: "Tweets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Retweeted",
                table: "Tweets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ScreenName",
                table: "Tweeters",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "FavouritesCount",
                table: "Tweeters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Hashtags",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Tweeters_TweeterId",
                table: "Tweets",
                column: "TweeterId",
                principalTable: "Tweeters",
                principalColumn: "TweeterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
