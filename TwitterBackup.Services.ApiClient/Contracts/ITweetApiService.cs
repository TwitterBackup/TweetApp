using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.ApiClient.Contracts
{
    public interface ITweetApiService
    {
        ApiTweetDto GetTweet(string tweetId);
    }
}
