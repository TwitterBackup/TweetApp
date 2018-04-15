using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitterBackup.DTO.Tweets
{
    public class TweetFromTwitterDto
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdString { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("entities")]
        public Entity Entity { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

    }


    public class Entity
    {
        [JsonProperty("urls")]
        public List<Url> Urls { get; set; }

        [JsonProperty("media")]
        public List<Media> Media { get; set; }
    }

    public class Media
    {
        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public string MediaUrlHttps { get; set; }
    }

    public class Url
    {
        [JsonProperty("url")]
        public string UrlString { get; set; }
    }
}
