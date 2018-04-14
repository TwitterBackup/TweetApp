using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackup.DTO.Tweets;

namespace TwitterBackup.Services.TwitterAPI
{
    public interface ITwitterService
    {
        string GetTweetsJson(string screenName);
        TweetFromTwitterDto CleanText(TweetFromTwitterDto tweet);
    }
}
