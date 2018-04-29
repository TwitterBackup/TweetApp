using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, IAuditable, IDeletable
    {
        public ApplicationUser()
        {
            this.UserTweets = new HashSet<UserTweet>();
            this.UserTweeters = new HashSet<UserTweeter>();
        }

        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SavedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserTweet> UserTweets { get; set; }

        public ICollection<UserTweeter> UserTweeters { get; set; }
    }
}
