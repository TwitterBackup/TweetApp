using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweeterService
    {
        Task<GetTweeterDto> GetTweeterByScreenNameAsync(string tweeterName);

        Task<IEnumerable<GetTweeterDto>> SearchTweetersAsync(string searchCriteria);
    }
}
