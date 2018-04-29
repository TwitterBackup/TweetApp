using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweetApiService
    {
        Task<ApiTweetDto> GetTweetByIdAsync(string tweetId);

        Task<IEnumerable<ApiTweetDto>> GetUserTimelineAsync(string tweeterName);

        Task<IEnumerable<ApiTweetDto>> SearchTweetsAsync(string searchCriteria);

        Task<RetweetResultDto> RetweetAsync(string tweetId);
    }
}
