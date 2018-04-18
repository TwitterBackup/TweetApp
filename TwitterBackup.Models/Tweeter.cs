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

        [Key]
        public string TweeterId { get; set; }

        public string CreatedAt { get; set; } //created_at

        public string Description { get; set; }

        public int FavouritesCount { get; set; } //Favourites_count 

        public int FollowersCount { get; set; } //Followers_count

        public int FriendsCount { get; set; } //Friends_count

        public string IdStr { get; set; } //Id_str

        public string Name { get; set; }

        public string ScreenName { get; set; } //Screen_name

        public string Lang { get; set; }

        public string Location { get; set; }

        public int TweetsCount { get; set; } //Statuses_count

        public bool Verified { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public ICollection<Tweet> Tweets { get; set; }

        public ICollection<UserTweeter> TweeterUsers { get; set; }

    }


}
