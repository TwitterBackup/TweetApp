using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Web.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        public string UserName { get; set; }

        [StringLength(15, MinimumLength = 2, ErrorMessage = "From 2 to 15 symbols, please!")]
        public string FirstName { get; set; }

        [StringLength(15, MinimumLength = 2, ErrorMessage = "From 2 to 15 symbols, please!")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}
