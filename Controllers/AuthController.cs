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

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authService = authService;
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
                return View(userVM);
            }


            AppUser newUser = authService.MapVMtoModel(userVM);

            var res = await userManager.CreateAsync(newUser, userVM.Password);

            if (!res.Succeeded)
            {
                foreach(var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(userVM);
            }

            var roleAssignRes = await userManager.AddToRoleAsync(newUser, "customer");

            if(!roleAssignRes.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(userVM);
            }


            await signInManager.SignInAsync(newUser, false);


            return View(userVM);
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
            }

            return View(user);
        }

        /*
           

        public IActionResult Logout()
        { 
        }
         
         */

    }
}
