using System;
using System.Collections.Generic;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    public class Tweet : IDeletable, IAuditable
    {
        public Tweet()
        {
            this.TweetUsers = new HashSet<UserTweet>();
            this.TweetHashtags = new HashSet<TweetHashtag>();
        }

        public string TweetId { get; set; }

        public string CreatedAt { get; set; } //Created_at

        public string IdStr { get; set; }

        public string Text { get; set; }

        public Tweeter Tweeter { get; set; } //User

        public int FavoriteCount { get; set; } //Favorite_count 

        public bool Favorited { get; set; }

        public string Lang { get; set; }

        public int QuoteCount { get; set; } //Quote_count

        public int RetweetCount { get; set; } //Retweet_count

        public bool Retweeted { get; set; }

        public Tweet RetweetedStatus { get; set; } //Retweeted_status 

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserTweet> TweetUsers { get; set; }

        public ICollection<TweetHashtag> TweetHashtags { get; set; }


    }
}
