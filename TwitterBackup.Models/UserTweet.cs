using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    public class UserTweet : IDeletable, IAuditable
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string TweetId { get; set; }

        [Required]
        public Tweet Tweet { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Please, 10 to 500 characters!")]
        public string TweetComments { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SavedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
