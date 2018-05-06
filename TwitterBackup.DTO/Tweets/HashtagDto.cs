using Newtonsoft.Json;

namespace TwitterBackup.DTO.Tweets
{
    public class HashtagDto
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
