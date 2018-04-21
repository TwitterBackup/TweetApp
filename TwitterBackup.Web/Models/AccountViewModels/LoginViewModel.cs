using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Username/Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
