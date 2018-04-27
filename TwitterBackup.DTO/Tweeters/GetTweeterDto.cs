using Newtonsoft.Json;

namespace TwitterBackup.DTO.Tweeters
{
    public class GetTweeterDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrlHttps { get; set; }

        public bool IsLikedFromUser { get; set; }
    }
}
