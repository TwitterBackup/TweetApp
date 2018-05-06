using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweetService
    {
        Task AddTweetForUserAsync(string userId, ApiTweetDto apiTweetDto);

        TweetDto GetTweetForUser(string userId, string tweetId);

        IEnumerable<TweetDto> GetAllTweetsForAdmin();

        IEnumerable<TweetDto> GetAllTweetsForUser(string userId);

        IEnumerable<TweetDto> GetAllTweetsByAuthorAsync(string tweeterId);

        IEnumerable<TweetDto> GetAllTweetsByTweeterForUser(string userId, string tweeterId);

        Task AddNoteToSavedTweetForUserAsync(string userId, string tweetId, string note);

        Task RemoveSavedTweetForUserAsync(string userId, string tweetId);

        Task RemoveSavedTweetForAllUsersAsync(string tweetId);

        IEnumerable<TweetDto> GetAllTweetsByTweeterForUser(string userId, string tweeterId);
    }
}
