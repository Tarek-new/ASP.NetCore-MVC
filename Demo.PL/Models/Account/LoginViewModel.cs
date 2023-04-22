using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invaild Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "Password minimum Lenght is 5")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }


    }
}
