using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class RemoveIdStr_from_Tweet_and_Tweeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdStr",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "IdStr",
                table: "Tweeters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdStr",
                table: "Tweets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdStr",
                table: "Tweeters",
                nullable: true);
        }
    }
}
