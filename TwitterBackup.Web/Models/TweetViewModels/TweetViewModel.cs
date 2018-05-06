using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models;

namespace TwitterBackup.Web.Models.TweetViewModels
{
    public class TweetViewModel
    {
        public string UserName { get; set; }

        public ApplicationUser User { get; set; }

        [Editable(false)]
        public string Hashtags { get; set; }

        [Editable(false)]
        public string TweeterName { get; set; }

        [Required]
        public string TweetId { get; set; } //Id_str


        [StringLength(300, MinimumLength = 3, ErrorMessage = "Please, 3 to 300 characters!")]
        public string TweetComments { get; set; }

        [Editable(false)]
        public string CreatedAt { get; set; } //Created_at

        [Required]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Please, 3 to 300 characters!")]
        public string Text { get; set; }

        [Editable(false)]
        public Tweeter Tweeter { get; set; } //User

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int FavoriteCount { get; set; } //Favorite_count 

        public string Language { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int QuoteCount { get; set; } //Quote_count

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int RetweetCount { get; set; } //Retweet_count

        public bool IsLikedFromUser { get; set; }
    }
}
