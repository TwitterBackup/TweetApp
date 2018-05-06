using System.Collections.Generic;
using TwitterBackup.Web.Models.TweetViewModels;

namespace TwitterBackup.Web.Models.TweeterViewModels
{
    public class TweeterProfileViewModel
    {
        public TweeterViewModel Tweeter { get; set; }

        public IEnumerable<TweetViewModel> SavedTweets { get; set; }

        public IEnumerable<TweetViewModel> NewTweets { get; set; }
    }
}
