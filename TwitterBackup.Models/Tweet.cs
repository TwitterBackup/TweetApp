using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string TweetId { get; set; } //Id_str

        [Required]
        public string CreatedAt { get; set; } //Created_at

        [Required]
        public string Text { get; set; }

        [Required]
        public Tweeter Tweeter { get; set; } //User

        public int FavoriteCount { get; set; } //Favorite_count 

        public string Lang { get; set; }

        public int QuoteCount { get; set; } //Quote_count

        public int RetweetCount { get; set; } //Retweet_count

        public Tweet RetweetedStatus { get; set; } //Retweeted_status 

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SavedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserTweet> TweetUsers { get; set; }

        public ICollection<TweetHashtag> TweetHashtags { get; set; }
    }
}
