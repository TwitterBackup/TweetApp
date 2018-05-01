using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models;

namespace TwitterBackup.Web.Models.TweeterViewModels
{
    public class TweeterViewModel
    {
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        public ApplicationUser User { get; set; }

        [StringLength(300, MinimumLength = 3, ErrorMessage = "Please, 3 to 300 characters!")]
        public string TweeterComments { get; set; }

        [Required]
        public string TweeterId { get; set; } //Id_str

        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Length should be between 1 and 50 characters")]
        public string ScreenName { get; set; } //Screen_name

        public string CreatedAt { get; set; } //created_at

        public string Description { get; set; }

        public int FollowersCount { get; set; } //Followers_count

        public int FriendsCount { get; set; } //Friends_count

        public string Language { get; set; }

        public string Location { get; set; }

        public int TweetsCount { get; set; } //Statuses_count

        public bool Verified { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SavedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public bool IsLikedFromUser { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ProfileBannerUrl { get; set; }
    }
}

