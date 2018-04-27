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

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int FavoriteCount { get; set; } //Favorite_count 

        [StringLength(10, MinimumLength = 2, ErrorMessage = "Parameter should be between 2 and 10 characters")]
        public string Language { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int QuoteCount { get; set; } //Quote_count

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int RetweetCount { get; set; } //Retweet_count

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
