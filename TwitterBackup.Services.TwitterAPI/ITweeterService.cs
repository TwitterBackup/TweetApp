using System.Collections.Generic;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.TwitterAPI
{
    public interface ITweeterService
    {
        GetTweeterDto GetTwitterByScreenName(string tweeterName);

        IEnumerable<GetTweeterDto> SearchTweeters(string searchCriteria);
    }
}
