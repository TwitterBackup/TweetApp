using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TwitterBackup.Models;

namespace TwitterBackup.DTO.User
{
    public class UserTweetDto
    {
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id_str")]
        public string TweetId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("User")]
        public Tweeter Tweeter { get; set; } //User

        [JsonProperty("Favorite_count")]
        public int FavoriteCount { get; set; } //Favorite_count 

        [JsonProperty("Quote_count")]
        public int QuoteCount { get; set; } //Quote_count

        [JsonProperty("Retweet_count")]
        public int RetweetCount { get; set; } //Retweet_count

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? SavedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
