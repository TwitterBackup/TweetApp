using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class TweetComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TweetComment",
                table: "UserTweets",
                newName: "TweetComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TweetComments",
                table: "UserTweets",
                newName: "TweetComment");
        }
    }
}
