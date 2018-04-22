using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.TwitterAPI.Contracts
{
    public interface ITweeterService
    {
        Task<TweeterDto> GetTweeterByScreenNameAsync(string tweeterName);

        Task<IEnumerable<TweeterDto>> SearchTweetersAsync(string searchCriteria);
    }
}
