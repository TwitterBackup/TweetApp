using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser, IAuditable, IDeletable
    {
        public User()
        {
            this.SavedTweets = new HashSet<UserTweet>();
            this.FavouriteTweeters = new HashSet<UserTweeter>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserTweet> SavedTweets { get; set; }

        public ICollection<UserTweeter> FavouriteTweeters { get; set; }
    }
}
