using System.Collections.Generic;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweetDbService
    {
        void Add(TweetDto tweet);

        void AddMany(IEnumerable<TweetDto> tweets);

        TweetDto GetById(string id);

        IEnumerable<TweetDto> GetTweetsByUser(UserDto user);

        IEnumerable<TweetDto> GetTweetsByTweeter(TweetDto user);

        void Update(TweetDto tweet);

        void Delete(TweetDto tweet);
    }
}
