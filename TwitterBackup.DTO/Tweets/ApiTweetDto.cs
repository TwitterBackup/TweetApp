using Newtonsoft.Json;
using System.Collections.Generic;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.DTO.Tweets
{
    public class ApiTweetDto
    {
        public string TweeterName { get; set; }

        [JsonProperty("hashtags")]
        public ICollection<HashtagDto> Hashtags { get; set; }

        public string TweetComments { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id_str")]
        public string TweetId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("User")]
        public TweeterDto Tweeter { get; set; } //User

        [JsonProperty("Favorite_count")]
        public int FavoriteCount { get; set; } //Favorite_count 

        [JsonProperty("Quote_count")]
        public int QuoteCount { get; set; } //Quote_count

        [JsonProperty("Retweet_count")]
        public int RetweetCount { get; set; } //Retweet_count
    }
}
