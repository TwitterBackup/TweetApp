using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Services.TwitterAPI.Contracts;

namespace TwitterBackup.Services.TwitterAPI
{
    public class TweetMessageService : ITweetMessageService
    {
        public Task<IEnumerable<TweetFromTwitterDto>> GetUserTimelineAsync(GetUserTimelineDto getUserTimelineDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TweetFromTwitterDto>> SearchTweetsAsync(SearchTweetDto searchTweetDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<RetweetResultDto> RetweetAsync(RetweetDto retweet)
        {
            throw new System.NotImplementedException();
        }
    }
}
