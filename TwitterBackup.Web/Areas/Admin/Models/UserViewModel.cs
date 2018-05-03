using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TwitterBackup.Models;

namespace TwitterBackup.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public IdentityUser IdentityUser { get; set; }

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

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}
