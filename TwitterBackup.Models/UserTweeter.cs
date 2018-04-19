using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Models
{
    public class UserTweeter
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string TweeterId { get; set; }

        [Required]
        public Tweeter Tweeter { get; set; }

    }
}
