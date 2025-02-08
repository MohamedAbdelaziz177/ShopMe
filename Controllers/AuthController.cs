using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.ViewModels.UserVM_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IAuthService authService;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              IAuthService authService,
                              RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authService = authService;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async  Task<IActionResult> SaveRegister(UserRegisterVM userVM)
        {
            if(!ModelState.IsValid) 
            {
                return View("Register", userVM);
            }


            AppUser newUser = authService.MapVMtoModel(userVM);

            var res = await userManager.CreateAsync(newUser, userVM.Password);

            if (!res.Succeeded)
            {
                foreach(var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

				return View("Register", userVM);
			}

            var roleAssignRes = await userManager.AddToRoleAsync(newUser, "Customer");

            if(!roleAssignRes.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

				return View("Register", userVM);
			}


            await signInManager.SignInAsync(newUser, false);


            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
       
        public IActionResult Login(string returnUrl) 
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveLogin(UserLoginVM user)
        {
            if (!ModelState.IsValid) 
                return View(user);
            

            AppUser userExists = await userManager.FindByEmailAsync(user.Email);

            if (userExists == null)
                return View(user);

            bool isValid = await userManager.CheckPasswordAsync(userExists, user.Password);

            if (isValid) 
            {
                await signInManager.SignInAsync(userExists, user.RememberMe);
                return RedirectToAction("Index", "Home");

            }

            return View(user);
        }

        
        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
		public async Task<IActionResult> GetProfile()
		{
            var appUser = await userManager.GetUserAsync(User);

            if (appUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

			var profileVM = authService.MapModelToVM(appUser);

			return View(profileVM);
		}

		/*
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {

        }

        */

		/*
         public async Task<IActionResult> SaveEditProfile(ProfileVM profile)
		{

		}
         */

		
		



	}
}
