﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TwitterBackup.Data;

namespace TwitterBackup.Data.Migrations
{
    [DbContext(typeof(TwitterDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TwitterBackup.Models.Hashtag", b =>
                {
                    b.Property<string>("HashtagId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime?>("SavedOn");

                    b.Property<string>("Text")
                        .HasMaxLength(300);

                    b.HasKey("HashtagId");

                    b.ToTable("Hashtags");
                });

            modelBuilder.Entity("TwitterBackup.Models.Tweet", b =>
                {
                    b.Property<string>("TweetId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedAt")
                        .IsRequired();

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("FavoriteCount");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lang");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("QuoteCount");

                    b.Property<int>("RetweetCount");

                    b.Property<string>("RetweetedStatusTweetId");

                    b.Property<DateTime?>("SavedOn");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("TweeterId")
                        .IsRequired();

                    b.HasKey("TweetId");

                    b.HasIndex("RetweetedStatusTweetId");

                    b.HasIndex("TweeterId");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("TwitterBackup.Models.Tweeter", b =>
                {
                    b.Property<string>("TweeterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedAt");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<int>("FollowersCount");

                    b.Property<int>("FriendsCount");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lang");

                    b.Property<string>("Location");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("SavedOn");

                    b.Property<string>("ScreenName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("TweetsCount");

                    b.Property<bool>("Verified");

                    b.HasKey("TweeterId");

                    b.ToTable("Tweeters");
                });

            modelBuilder.Entity("TwitterBackup.Models.TweetHashtag", b =>
                {
                    b.Property<string>("TweetId");

                    b.Property<string>("HashtagId");

                    b.HasKey("TweetId", "HashtagId");

                    b.HasIndex("HashtagId");

                    b.ToTable("TweetHashtags");
                });

            modelBuilder.Entity("TwitterBackup.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(20);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime?>("SavedOn");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TwitterBackup.Models.UserTweet", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TweetId");

                    b.HasKey("UserId", "TweetId");

                    b.HasIndex("TweetId");

                    b.ToTable("UserTweets");
                });

            modelBuilder.Entity("TwitterBackup.Models.UserTweeter", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TweeterId");

                    b.HasKey("UserId", "TweeterId");

                    b.HasIndex("TweeterId");

                    b.ToTable("UserTweeters");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TwitterBackup.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TwitterBackup.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TwitterBackup.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TwitterBackup.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Models.Tweet", b =>
                {
                    b.HasOne("TwitterBackup.Models.Tweet", "RetweetedStatus")
                        .WithMany()
                        .HasForeignKey("RetweetedStatusTweetId");

                    b.HasOne("TwitterBackup.Models.Tweeter", "Tweeter")
                        .WithMany("Tweets")
                        .HasForeignKey("TweeterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Models.TweetHashtag", b =>
                {
                    b.HasOne("TwitterBackup.Models.Hashtag", "Hashtag")
                        .WithMany("HashtagTweets")
                        .HasForeignKey("HashtagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TwitterBackup.Models.Tweet", "Tweet")
                        .WithMany("TweetHashtags")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Models.UserTweet", b =>
                {
                    b.HasOne("TwitterBackup.Models.Tweet", "Tweet")
                        .WithMany("TweetUsers")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TwitterBackup.Models.User", "User")
                        .WithMany("SavedTweets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Models.UserTweeter", b =>
                {
                    b.HasOne("TwitterBackup.Models.Tweeter", "Tweeter")
                        .WithMany("TweeterUsers")
                        .HasForeignKey("TweeterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TwitterBackup.Models.User", "User")
                        .WithMany("FavouriteTweeters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
