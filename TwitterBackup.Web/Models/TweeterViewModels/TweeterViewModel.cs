namespace TwitterBackup.Web.Models.TweeterViewModels
{
    public class TweeterViewModel
    {
        public string TweeterId { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileBannerUrl { get; set; }

        public string CreatedAt { get; set; }

        public string Description { get; set; }

        public int FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public string Lang { get; set; }

        public string Location { get; set; }

        public int TweetsCount { get; set; }

        public bool Verified { get; set; }

        public bool IsLikedFromUser { get; set; }

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
