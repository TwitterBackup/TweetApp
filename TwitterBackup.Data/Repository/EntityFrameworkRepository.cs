using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDeletable
    {
        private readonly TwitterDbContext context;
        private readonly DbSet<TEntity> entities;

        public EntityFrameworkRepository(TwitterDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.entities = context.Set<TEntity>() ??
                             throw new ArgumentNullException(nameof(EntityFrameworkRepository<TEntity>.entities));
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return entities.Any(predicate);
        }

        public TEntity GetById(string id)
        {
            return entities.Find(id);
        }

        public TEntity GetByCompositeId(string id1, string id2)
        {
            return entities.Find(id1, id2);
        }

        //da se premesti
        public string GetTweetHashtagsByTweetId(string tweetId)
        {
            var tweetHashtags = this.context.TweetHashtags
            .Include(ctx => ctx.Tweet)
            .Include(ctx => ctx.Hashtag)
            .Where(tweetHashtag => tweetHashtag.TweetId == tweetId)
            .ToList();

            var hashtags = new StringBuilder();
            foreach (var tweetHashtag in tweetHashtags)
            {
                if (tweetHashtag != null)
                    hashtags.Append(tweetHashtag.Hashtag.Text + "; ");
            }

            return hashtags.ToString();

        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await entities.FindAsync(id);
        }

        //ne trqbva da e user tweet conkretika
        public UserTweet GetResourceByIdPerUser(string resourceId, string userId)
        {
            var result = context.UserTweets
                .Include(ctx => ctx.User)
                .Include(ctx => ctx.Tweet)
                .Include(ctx => ctx.Tweet.Tweeter)
                .Include(ctx => ctx.Tweet.TweetHashtags)
                .SingleOrDefault(userTweets => userTweets.TweetId == resourceId && userTweets.UserId == userId);

            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> GetAllAsync(IEnumerable<TEntity> collection)
        {
            return collection.Where(entity => entity.IsDeleted == false).ToList();
            //return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync(); ;
        }


        public IEnumerable<UserTweet> GetAllUsersWithTweetsAsync(IEnumerable<UserTweet> twee)
        {
            //var result = await context.UserTweets
            //    .Include(ctx => ctx.User)
            //    .Include(ctx => ctx.Tweet)
            //    .Include(ctx => ctx.Tweet.Tweeter)
            //    .Include(ctx => ctx.Tweet.TweetHashtags)
            //    .Where(tweet => tweet.IsDeleted == false)
            //    .ToListAsync();

            return twee.Where(tweet => tweet.IsDeleted == false).ToList();
        }


        public IEnumerable<TEntity> IncludeDbSet(params Expression<Func<TEntity, object>>[] includes)
        //public IQueryable<TEntity> IncludeDbSet(params Expression<Func<TEntity, object>>[] includes)
        {
            IIncludableQueryable<TEntity, object> query = null;

            if (includes.Length > 0)
            {
                query = entities.Include(includes[0]);
            }
            for (var queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = (query ?? throw new ArgumentNullException()).Include(includes[queryIndex]);
            }

            return query == null ? entities : (IEnumerable<TEntity>)query;
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {

            return entities.Where(predicate);
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
            var result = await entities
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
            return entities.SingleOrDefault(predicate);
        }

        //public void Add(TEntity entity)
        //{
        //    entities.Add(entity);
        //}

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entities.Add(entity);
            // await context.SaveChangesAsync();
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            entities.Remove(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            entities.Remove(entity);
            await context.SaveChangesAsync();

        }
    }
}

