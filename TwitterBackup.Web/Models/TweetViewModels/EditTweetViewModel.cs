using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Web.Models.TweetViewModels
{
    public class EditTweetViewModel
    {
        public string UserName { get; set; }

        [Required]
        public string TweetId { get; set; } //Id_str

        public string Text { get; set; }

        [StringLength(300, MinimumLength = 3, ErrorMessage = "Please, 3 to 300 characters!")]
        public string TweetComments { get; set; }
    }
}
