using System.ComponentModel;

namespace TwitterBackup.Web.Areas.Admin.Models
{
    public class StatisticsViewModel
    {
        [DisplayName("UserName")]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Number of Favorite Tweeters")]
        public int NumberOfTweeters { get; set; }

        [DisplayName("Number of Saved Tweets")]
        public int NumberOfTweets { get; set; }

        [DisplayName("Number of ReTweets")]
        public int NumberOfRetweets { get; set; }

        [DisplayName("Total Favorite Tweeters")]
        public int TotalTweeters { get; set; }

        [DisplayName("Total Saved Tweets")]
        public int TotalTweets { get; set; }

        [DisplayName("Total ReTweets")]
        public int TotalRetweets { get; set; }

    }
}
