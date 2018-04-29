using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweetApiService
    {
        Task<IEnumerable<TweetDto>> GetUserTimelineAsync(GetUserTimelineDto getUserTimelineDto);

        Task<IEnumerable<TweetDto>> SearchTweetsAsync(SearchTweetDto searchTweetDto);

        Task<RetweetResultDto> RetweetAsync(RetweetDto retweet);

        ApiTweetDto GetTweet(string tweetId);

    }
}
