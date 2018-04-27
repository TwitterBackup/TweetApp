using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweetDbService
    {
        void AddTweetToDb(TweetDto tweet);

        void AddManyTweetsToDb(IEnumerable<TweetDto> tweets);

        TweetDto TweetById(string tweetId, string userId);

        Task<IEnumerable<TweetDto>> GetAllTweetsAsync();

        Task<IEnumerable<TweetDto>> GetTweetsByUserIdAsync(string userId);

        IEnumerable<TweetDto> GetTweetsByTweeterIdAsync(string id);

        void UpdateTweet(EditTweetDto editTweetDto, string userId);

        void SoftDeleteTweetByIdPerUserId(string tweetId, string userId);

        void SoftDeleteTweetByIdForAllUsers(string tweetId);
    }
}
