using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TwitterBackup.Models;

namespace TwitterBackup.DTO.User
{
    public class UserDto
    {
        public string FirstName { get; set; }

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

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}
