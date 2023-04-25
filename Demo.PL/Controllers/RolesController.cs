using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles= _roleManager.Roles.ToList();
            return View(roles);
        }


        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create( IdentityRole identityRole)
        {
           if(ModelState.IsValid)
            {
                var result= await _roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var error in  result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }    
                }

            }
            return View();
        }




        public IActionResult Details(string id)
        {
            return View();
        }
    }
}
