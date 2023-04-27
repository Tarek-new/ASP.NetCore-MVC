using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Demo.PL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string SearchValue = "")
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var users = await _userManager.Users.Select(user => new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = _userManager.GetRolesAsync(user).Result// can't use await when assigning values !!!

                }).ToListAsync();

                return View(users);
            }

            else
            {
                var user = await _userManager.FindByEmailAsync(SearchValue.Trim());
                if (user == null)
                {
                    ModelState.AddModelError("", "Email Not Found");
                    return View(new List<UserViewModel>());

                }
                else
                {
                    var userViewModel = new UserViewModel()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Roles = _userManager.GetRolesAsync(user).Result
                    };
                    return View(new List<UserViewModel>() { userViewModel });

                }

            }

        }


        #region Update User
        public async Task<IActionResult> Update(string id)
        {

            if (id is null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };


            return View(userViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, UserViewModel userViewModel)
        {

            if (id != userViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.UserName = userViewModel.UserName;
                    user.NormalizedUserName = userViewModel.UserName.ToUpper();
                    user.Email = userViewModel.Email;
                    user.PhoneNumber = userViewModel.PhoneNumber;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction("Index");

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);


                }
                catch (System.Exception)
                {

                    return BadRequest();
                }
            }
            return View();
        }


        #endregion

        #region Delete User
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null) return NotFound();



            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");


        }

        #endregion

        #region Details 
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return NotFound();


            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);

        }
        #endregion

        #region Roles management

        [Authorize (Roles="Admin")]
        public async Task<IActionResult> ManageRoles(string Id)
        {

            var user = await _userManager.FindByIdAsync(Id);

            if (user == null) return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            if (!roles.Any()) return NotFound();

            var userRolesViewModel = new UserRolesViewModel()
            {

                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {

                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result


                }).ToList()

            };

            return View(userRolesViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user == null) return NotFound();

                var userRole = await _userManager.GetRolesAsync(user);

               foreach(var role in model.Roles)
                {
                    if(userRole.Any(r=>r==role.RoleName) && !role.IsSelected)
                        await _userManager.RemoveFromRoleAsync(user,role.RoleName);

                    if(!userRole.Any(r=>r==role.RoleName) && role.IsSelected)
                        await _userManager.AddToRoleAsync(user,role.RoleName);

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }

        #endregion

    }



}
