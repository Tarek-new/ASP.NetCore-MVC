using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "Password minimum Lenght is 5")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
