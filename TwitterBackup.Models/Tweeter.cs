using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    public class Tweeter : IDeletable, IAuditable
    {
        public Tweeter()
        {
            this.Tweets = new HashSet<Tweet>();
            this.TweeterUsers = new HashSet<UserTweeter>();
        }

        [Required]
        public string TweeterId { get; set; } //Id_str

        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Length should be between 1 and 50 characters")]
        public string ScreenName { get; set; } //Screen_name

        [StringLength(50, MinimumLength = 6, ErrorMessage = "Parameter should be between 6 and 50 characters")]
        public string CreatedAt { get; set; } //created_at

        [StringLength(200, MinimumLength = 10, ErrorMessage = "Parameter should be between 10 and 200 characters")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int FollowersCount { get; set; } //Followers_count

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int FriendsCount { get; set; } //Friends_count

        [StringLength(10, MinimumLength = 2, ErrorMessage = "Parameter should be between 2 and 10 characters")]
        public string Language { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Parameter should be between 2 and 50 characters")]
        public string Location { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int TweetsCount { get; set; } //Statuses_count

        public bool Verified { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SavedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<Tweet> Tweets { get; set; }

        public ICollection<UserTweeter> TweeterUsers { get; set; }

    }


}
