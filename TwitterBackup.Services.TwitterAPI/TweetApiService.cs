﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Services.TwitterAPI.Contracts;

namespace TwitterBackup.Services.TwitterAPI
{
    public class TweetApiService : ITweetApiService
    {
        public Task<IEnumerable<TweetDto>> GetUserTimelineAsync(GetUserTimelineDto getUserTimelineDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TweetDto>> SearchTweetsAsync(SearchTweetDto searchTweetDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<RetweetResultDto> RetweetAsync(RetweetDto retweet)
        {
            throw new System.NotImplementedException();
        }

        public ApiTweetDto GetTweet(string tweetId)
        {
            throw new System.NotImplementedException();
        }
    }
}