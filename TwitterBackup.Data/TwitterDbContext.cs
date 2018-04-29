using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TwitterBackup.Models;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Data
{
    public class TwitterDbContext : IdentityDbContext<ApplicationUser>
    {
        public TwitterDbContext(DbContextOptions<TwitterDbContext> options)
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


            //builder.ApplyConfiguration()

            //define many 2 many - User - Tweet
            builder.Entity<UserTweet>()
                .HasKey(ut => new { ut.UserId, ut.TweetId });

            builder.Entity<UserTweet>()
                .HasOne(ut => ut.User)
                .WithMany(t => t.UserTweets)
                .HasForeignKey(ut => ut.UserId);

            builder.Entity<UserTweet>()
                .HasOne(ut => ut.Tweet)
                .WithMany(u => u.UserTweets)
                .HasForeignKey(ut => ut.TweetId);


            //define many 2 many - User - Tweeter
            builder.Entity<UserTweeter>()
                .HasKey(ut => new { ut.UserId, ut.TweeterId });

            builder.Entity<UserTweeter>()
                .HasOne(ut => ut.User)
                .WithMany(t => t.UserTweeters)
                .HasForeignKey(ut => ut.UserId);

            builder.Entity<UserTweeter>()
                .HasOne(ut => ut.Tweeter)
                .WithMany(u => u.UserTweeters)
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
                .WithMany(t => t.TweetHashtags)
                .HasForeignKey(th => th.HashtagId);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(x => x.UserName).IsUnique();
                entity.HasIndex(x => x.Email).IsUnique();
            });
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.SavedOn == null)
                {
                    entity.SavedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
