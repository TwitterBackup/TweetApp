using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Migrations
{
    public partial class Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hashtags",
                columns: table => new
                {
                    HashtagId = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtags", x => x.HashtagId);
                });

            migrationBuilder.CreateTable(
                name: "Tweeters",
                columns: table => new
                {
                    TweeterId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FavouritesCount = table.Column<int>(nullable: false),
                    FollowersCount = table.Column<int>(nullable: false),
                    FriendsCount = table.Column<int>(nullable: false),
                    IdStr = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Lang = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ScreenName = table.Column<string>(nullable: true),
                    TweetsCount = table.Column<int>(nullable: false),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweeters", x => x.TweeterId);
                });

            migrationBuilder.CreateTable(
                name: "Tweets",
                columns: table => new
                {
                    TweetId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    FavoriteCount = table.Column<int>(nullable: false),
                    Favorited = table.Column<bool>(nullable: false),
                    IdStr = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Lang = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    QuoteCount = table.Column<int>(nullable: false),
                    RetweetCount = table.Column<int>(nullable: false),
                    Retweeted = table.Column<bool>(nullable: false),
                    RetweetedStatusTweetId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    TweeterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweets", x => x.TweetId);
                    table.ForeignKey(
                        name: "FK_Tweets_Tweets_RetweetedStatusTweetId",
                        column: x => x.RetweetedStatusTweetId,
                        principalTable: "Tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tweets_Tweeters_TweeterId",
                        column: x => x.TweeterId,
                        principalTable: "Tweeters",
                        principalColumn: "TweeterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTweeters",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TweeterId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTweeters", x => new { x.UserId, x.TweeterId });
                    table.ForeignKey(
                        name: "FK_UserTweeters_Tweeters_TweeterId",
                        column: x => x.TweeterId,
                        principalTable: "Tweeters",
                        principalColumn: "TweeterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTweeters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TweetHashtags",
                columns: table => new
                {
                    TweetId = table.Column<string>(nullable: false),
                    HashtagId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetHashtags", x => new { x.TweetId, x.HashtagId });
                    table.ForeignKey(
                        name: "FK_TweetHashtags_Hashtags_HashtagId",
                        column: x => x.HashtagId,
                        principalTable: "Hashtags",
                        principalColumn: "HashtagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetHashtags_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTweets",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TweetId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTweets", x => new { x.UserId, x.TweetId });
                    table.ForeignKey(
                        name: "FK_UserTweets_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTweets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TweetHashtags_HashtagId",
                table: "TweetHashtags",
                column: "HashtagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_RetweetedStatusTweetId",
                table: "Tweets",
                column: "RetweetedStatusTweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_TweeterId",
                table: "Tweets",
                column: "TweeterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTweeters_TweeterId",
                table: "UserTweeters",
                column: "TweeterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTweets_TweetId",
                table: "UserTweets",
                column: "TweetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TweetHashtags");

            migrationBuilder.DropTable(
                name: "UserTweeters");

            migrationBuilder.DropTable(
                name: "UserTweets");

            migrationBuilder.DropTable(
                name: "Hashtags");

            migrationBuilder.DropTable(
                name: "Tweets");

            migrationBuilder.DropTable(
                name: "Tweeters");
        }
    }
}
