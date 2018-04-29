using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IDeletable
    {
        bool Any(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(string id);

        Task<TEntity> GetByIdAsync(string id);

        //Da otide na konkretnoto mqsto
        //UserTweet GetResourceByIdPerUser(string tweetId, string userId);

        TEntity GetByCompositeId(string id1, string id2);

        //string GetTweetHashtagsByTweetId(string tweetId);

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetAllAsync(IEnumerable<TEntity> collection);

        //IEnumerable<UserTweet> GetAllUsersWithTweetsAsync(IEnumerable<UserTweet> twee);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        //Task<IEnumerable<UserTweet>> FindTweetsAsync(Expression<Func<UserTweet, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        //void Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        void Remove(TEntity entity);

        Task RemoveAsync(TEntity entity);

        //IQueryable<TEntity> IncludeDbSet(params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> IncludeDbSet(params Expression<Func<TEntity, object>>[] includes);

        string GetTweetHashtagsByTweetId(string tweetId);

    }
}
