using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterBackup.Models
{
    public class UserTweeter
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string TweeterId { get; set; }

        public Tweeter Tweeter { get; set; }

    }
}
