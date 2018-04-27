using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwitterBackup.Models;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IDeletable
    {
        TEntity GetById(string id);
        Task<TEntity> GetByIdAsync(string id);

        UserTweet GetTweetById(string tweetId, string userId);

        TEntity GetByCompositeId(string id1, string id2);

        string GetTweetHashtagsByTweetId(string tweetId);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(int pageIndex, int pageSize = 10);

        Task<IEnumerable<UserTweet>> GetAllUsersWithTweetsAsync(int pageIndex, int pageSize = 10);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<UserTweet>> FindTweetsAsync(Expression<Func<UserTweet, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveRangeAsync(IEnumerable<TEntity> entity);
    }
}
