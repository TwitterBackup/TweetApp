using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class RemovedRetweetedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Tweets_RetweetedStatusTweetId",
                table: "Tweets");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_RetweetedStatusTweetId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "RetweetedStatusTweetId",
                table: "Tweets");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RetweetedStatusTweetId",
                table: "Tweets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_RetweetedStatusTweetId",
                table: "Tweets",
                column: "RetweetedStatusTweetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Tweets_RetweetedStatusTweetId",
                table: "Tweets",
                column: "RetweetedStatusTweetId",
                principalTable: "Tweets",
                principalColumn: "TweetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
