using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Models
{
    public class UserTweet
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string TweetId { get; set; }

        [Required]
        public Tweet Tweet { get; set; }
    }
}
