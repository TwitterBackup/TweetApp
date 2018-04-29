using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Web.Models.TweeterViewModels
{
    public class EditTweeterViewModel
    {
        public string UserName { get; set; }

        [Required]
        public string TweeterId { get; set; } //Id_str

        public string Description { get; set; }

        [StringLength(300, MinimumLength = 3, ErrorMessage = "Please, 3 to 300 characters!")]
        public string TweeterComments { get; set; }
    }
}
