using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class SmallChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lang",
                table: "Tweets",
                newName: "Language");

            migrationBuilder.RenameColumn(
                name: "Lang",
                table: "Tweeters",
                newName: "Language");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Tweets",
                newName: "Lang");

            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Tweeters",
                newName: "Lang");
        }
    }
}
