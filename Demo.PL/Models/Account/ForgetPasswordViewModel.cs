using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.Account
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
