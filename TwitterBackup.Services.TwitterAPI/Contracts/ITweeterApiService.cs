using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweeterApiService
    {
        Task<GetTweeterDto> GetTweeterByScreenNameAsync(string tweeterName);

        Task<IEnumerable<GetTweeterDto>> SearchTweetersAsync(string searchCriteria);
    }
}
