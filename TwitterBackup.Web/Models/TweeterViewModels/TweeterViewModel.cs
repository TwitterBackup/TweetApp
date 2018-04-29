using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models;

namespace TwitterBackup.Web.Models.TweeterDbViewModel
{
    public class TweeterViewModel
    {
        public string UserName { get; set; }

        public ApplicationUser User { get; set; }

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

        public string Lang { get; set; }

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

    }
}
