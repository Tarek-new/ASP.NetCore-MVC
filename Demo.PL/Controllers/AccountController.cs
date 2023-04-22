using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager=userManager;
            _signInManager = signInManager;
        }

        #region SignUp

       public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    UserName=registerViewModel.UserName,
                    IsAgree = registerViewModel.IsAgree
                };
                var result= await _userManager.CreateAsync(user, registerViewModel.Password);
                if(result.Succeeded)
                    return RedirectToAction("SignIn");

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
            }
            return View(registerViewModel);
        }

        #endregion


        #region SignIn

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
       public async Task<IActionResult> SignIn( LoginViewModel loginViewModel )
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if(user is not null)
                {
                    var password= await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (password)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password
                            , loginViewModel.RememberMe, false);

                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                       
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                       
                       
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Email");
                    }
                    
                }
                
            }
            return View(loginViewModel);


        }

        #endregion

        #region SignOut

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion




        #region Forgetpass

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword( ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user is not not null)
                {
                    var token= await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.ActionLink("ResetPassword", "Account", 
                        new { Email = model.Email, Token = token },Request.Scheme);


                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = resetPasswordLink,
                        To = model.Email
                    };

                    // To Do => SendEmail Method

                    EmailSettings.SendEmail(email);
                    return RedirectToAction("Complete Forget Password");
                }

                ModelState.AddModelError("", "email not found");
            }


            return View();
        }








        #endregion



        #region Resetpass

        public IActionResult ResetPassword( string email , string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                        RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Password");
                }
            }


            return View();
        }








        #endregion





        public IActionResult ResetPasswordDone()
        {
            return View();
        }
    }
}
