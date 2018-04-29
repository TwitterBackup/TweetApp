using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TwitterBackup.Models;

namespace TwitterBackup.DTO.Tweets
{
    public class ApiTweetDto
    {
        public string UserName { get; set; }

        public ApplicationUser User { get; set; }

        public string TweeterName { get; set; }

        [JsonProperty("hashtags")]
        public ICollection<Hashtag> Hashtags { get; set; }

        public string TweetComments { get; set; }

        public string ErrorMessage { get; set; }

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

        //public bool IsDeleted { get; set; }

        //public DateTime? DeletedOn { get; set; }

        //public DateTime? SavedOn { get; set; }

        //public DateTime? ModifiedOn { get; set; }
    }
}
