using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.TwitterAPI
{
    public interface ITweeterService
    {
        Task<GetTweeterDto> GetTwitterByScreenNameAsync(string tweeterName);

        Task<IEnumerable<GetTweeterDto>> SearchTweetersAsync(string searchCriteria);
    }
}
