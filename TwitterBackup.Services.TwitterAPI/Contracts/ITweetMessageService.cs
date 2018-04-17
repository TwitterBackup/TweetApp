using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweetMessageService
    {
        Task<IEnumerable<TweetFromTwitterDto>> GetUserTimelineAsync(GetUserTimelineDto getUserTimelineDto);

        Task<IEnumerable<TweetFromTwitterDto>> SearchTweetsAsync(SearchTweetDto searchTweetDto);

        Task<RetweetResultDto> RetweetAsync(RetweetDto retweet);
    }
}
