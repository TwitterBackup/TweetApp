using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
