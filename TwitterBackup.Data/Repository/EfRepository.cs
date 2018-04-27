using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Models;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Data.Repository
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDeletable
    {
        private readonly TwitterDbContext context;
        private readonly DbSet<TEntity> TEntities;

        public EfRepository(TwitterDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.TEntities = context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(EfRepository<TEntity>.TEntities));
        }

        public TEntity GetById(string id)
        {
            return TEntities.Find(id);
        }

        public TEntity GetByCompositeId(string id1, string id2)
        {
            return TEntities.Find(id1, id2);
        }

        public string GetTweetHashtagsByTweetId(string tweetId)
        {
            var hashtags = new StringBuilder();

            var tweetHashtags = this.context.TweetHashtags
                .Include(ctx => ctx.Tweet)
                .Include(ctx => ctx.Hashtag)
                .Where(tweetHashtag => tweetHashtag.TweetId == tweetId)
                .ToList();

            foreach (var tweetHashtag in tweetHashtags)
            {
                if (tweetHashtag != null)
                    hashtags.Append(tweetHashtag.Hashtag.Text + "; ");
            }

            return hashtags.ToString();

        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await TEntities.FindAsync(id);
        }

        public UserTweet GetTweetById(string tweetId, string userId)
        {
            var result = context.UserTweets
                .Include(ctx => ctx.User)
                .Include(ctx => ctx.Tweet)
                .Include(ctx => ctx.Tweet.Tweeter)
                .Include(ctx => ctx.Tweet.TweetHashtags)
                .SingleOrDefault(userTweets => userTweets.TweetId == tweetId && userTweets.UserId == userId);

            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageIndex, int pageSize = 10)
        {
            var result = await context.Set<TEntity>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<UserTweet>> GetAllUsersWithTweetsAsync(int pageIndex = 1, int pageSize = 10)
        {
            var result = await context.UserTweets
                .Include(ctx => ctx.User)
                .Include(ctx => ctx.Tweet)
                .Include(ctx => ctx.Tweet.Tweeter)
                .Include(ctx => ctx.Tweet.TweetHashtags)
                .Where(tweet => tweet.IsDeleted == false)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return TEntities.Where(predicate);
        }


        public Tweet FindTweet(string tweetId)
        {
            var result = context.Tweets
                .Include(ctx => ctx.Tweeter)
                .SingleOrDefault(tweets => tweets.TweetId == tweetId);

            return result;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await TEntities
                .Where(predicate)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<UserTweet>> FindTweetsAsync(Expression<Func<UserTweet, bool>> predicate)
        {
            var result = await context.UserTweets
                .Include(ctx => ctx.User)
                .Include(ctx => ctx.Tweet)
                .Include(ctx => ctx.Tweet.Tweeter)
                .Where(predicate)
                .ToListAsync();
            return result;
        }


        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return TEntities.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            TEntities.Add(entity);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            TEntities.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            TEntities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            TEntities.Remove(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            TEntities.Remove(entity);
            await context.SaveChangesAsync();

        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            TEntities.RemoveRange(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }

}

