using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweetMessageService
    {
        Task<TweetFromTwitterDto> GetTweetById(string tweetId);

        Task<IEnumerable<TweetFromTwitterDto>> GetUserTimelineAsync(string tweeterName);

        Task<IEnumerable<TweetFromTwitterDto>> SearchTweetsAsync(string searchCriteria);

        Task<RetweetResultDto> RetweetAsync(string tweetId);
    }
}
