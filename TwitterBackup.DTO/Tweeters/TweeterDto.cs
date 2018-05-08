using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models;

namespace TwitterBackup.DTO.Tweeters
{
    public class TweeterDto
    {
        private string profileImageUrl;

        public string UserName { get; set; }

        public ApplicationUser User { get; set; }

        [StringLength(300, MinimumLength = 3, ErrorMessage = "Please, 3 to 300 characters!")]
        public string TweeterComments { get; set; }

        [JsonProperty("id_str")]
        public string TweeterId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrl
        {
            get => this.profileImageUrl;
            set => this.profileImageUrl = value?.Replace("_normal", "");
        }

        [JsonProperty("profile_banner_url")]
        public string ProfileBannerUrl { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("statuses_count")]
        public int TweetsCount { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        public override int GetHashCode()
        {
            if (this.TweeterId == null)
            {
                return 0;
            }

            return this.TweeterId.GetHashCode();
        }
    }
}
