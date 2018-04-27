using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweetMessageService
    {
        Task<IEnumerable<TweetDto>> GetUserTimelineAsync(GetUserTimelineDto getUserTimelineDto);

        Task<IEnumerable<TweetDto>> SearchTweetsAsync(SearchTweetDto searchTweetDto);

        Task<RetweetResultDto> RetweetAsync(RetweetDto retweet);
    }
}
