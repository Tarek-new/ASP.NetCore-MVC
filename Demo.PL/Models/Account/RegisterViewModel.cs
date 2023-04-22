using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage = "Invaild Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5,ErrorMessage = "Password minimum Lenght is 5")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserName { get; set; }

        public bool IsAgree { get; set; }
    }
}
