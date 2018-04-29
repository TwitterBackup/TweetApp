using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    public class UserTweeter: IDeletable, IAuditable
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string TweeterId { get; set; }

        [Required]
        public Tweeter Tweeter { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Please, 10 to 500 characters!")]
        public string TweeterComments { get; set; }


        [StringLength(500, MinimumLength = 10, ErrorMessage = "Please, 10 to 500 characters!")]
        public string TweeterComment { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SavedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
