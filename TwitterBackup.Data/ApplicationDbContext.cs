using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TwitterBackup.Models;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Tweeter> Tweeters { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<TweetHashtag> TweetHashtags { get; set; }
        public DbSet<UserTweet> UserTweets { get; set; }
        public DbSet<UserTweeter> UserTweeters { get; set; }


        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            //define many 2 many - User - Tweet
            builder.Entity<UserTweet>()
                .HasKey(ut => new { ut.UserId, ut.TweetId });

            builder.Entity<UserTweet>()
                .HasOne(ut => ut.User)
                .WithMany(t => t.SavedTweets)
                .HasForeignKey(ut => ut.UserId);

            builder.Entity<UserTweet>()
                .HasOne(ut => ut.Tweet)
                .WithMany(u => u.TweetUsers)
                .HasForeignKey(ut => ut.TweetId);


            //define many 2 many - User - Tweeter
            builder.Entity<UserTweeter>()
                .HasKey(ut => new { ut.UserId, ut.TweeterId });

            builder.Entity<UserTweeter>()
                .HasOne(ut => ut.User)
                .WithMany(t => t.FavouriteTweeters)
                .HasForeignKey(ut => ut.UserId);

            builder.Entity<UserTweeter>()
                .HasOne(ut => ut.Tweeter)
                .WithMany(u => u.TweeterUsers)
                .HasForeignKey(ut => ut.TweeterId);

            //define many 2 many - Tweet - Hashtag
            builder.Entity<TweetHashtag>()
                .HasKey(th => new { th.TweetId, th.HashtagId });

            builder.Entity<TweetHashtag>()
                .HasOne(th => th.Tweet)
                .WithMany(h => h.TweetHashtags)
                .HasForeignKey(th => th.TweetId);

            builder.Entity<TweetHashtag>()
                .HasOne(th => th.Hashtag)
                .WithMany(t => t.HashtagTweets)
                .HasForeignKey(th => th.HashtagId);

        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
