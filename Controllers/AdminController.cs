using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.Services.MServices;
using E_Commerce2.ViewModels.UserVM_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Controllers
{
	[Authorize(Roles = "Admin")]
	//[Route("/Admin/[controller]/[action = Index]/{id?}")]
	public class AdminController : Controller
	{
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IAuthService authService;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager
                               , SignInManager<AppUser> signInManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.authService = authService;
        }


		public IActionResult GetAllUsers()
		{
            var allUsers = userManager.Users.ToList();
			return View(allUsers);
		}

		public async Task<IActionResult> GetUserById(string id) 
		{
            var user = await userManager.FindByIdAsync(id);
            return View(user);
		}

		public async Task<IActionResult> DeleteUser(string id) 
		{
            AppUser user = await userManager.FindByIdAsync(id);

            if (user == null) 
            {
                ViewBag.ErrorMessage = "This id is not valid";
                return RedirectToAction("GetAllUsers");
            }

			ViewBag.SuccessMessage = "This User is deleted successfully";
			await userManager.DeleteAsync(user);

            return RedirectToAction("GetAllUsers");
        }

        


		[HttpGet]
		public IActionResult AddNewAdmin()
		{
			return View();
		}



		[HttpPost]
        public async Task<IActionResult> SaveNewAdmin(UserRegisterVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View("AddNewAdmin", userVM);
            }


            AppUser newUser = authService.MapVMtoModel(userVM);

            var res = await userManager.CreateAsync(newUser, userVM.Password);

            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View("AddNewAdmin", userVM);
            }

            var roleAssignRes = await userManager.AddToRoleAsync(newUser, "Admin");

            if (!roleAssignRes.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View("AddNewAdmin", userVM);
            }


            await signInManager.SignInAsync(newUser, false);


            return RedirectToAction("Index", "Home");
        }


    }
}
