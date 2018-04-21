using System.Collections.Generic;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweetDbService
    {
        void AddTweetToDb(TweetDto tweet);

        void AddManyTweetsToDb(IEnumerable<TweetDto> tweets);

        TweetDto GetTweetById(string id);

        IEnumerable<TweetDto> GetTweetsByUser(UserDto user);

        IEnumerable<TweetDto> GetTweetsByTweeterIdAsync(string id);

        void UpdateTweet(TweetDto tweet);

        void DeleteTweet(TweetDto tweet);
    }
}
