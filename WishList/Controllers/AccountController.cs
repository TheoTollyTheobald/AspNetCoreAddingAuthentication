using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WishList.Models;
using WishList.Models.AccountViewModels;
using WishList.Controllers;

namespace WishList.Controllers
{
    [Authorize] 
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        private IActionResult Register(RegisterViewModel registerViewModel)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };
            var userResult = _userManager.CreateAsync(new ApplicationUser() { Email = registerViewModel.Email, UserName = registerViewModel.Email }, registerViewModel.Password).Result;
            if (!userResult.Succeeded)
            {
                foreach (var error in userResult.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                return View("Register", registerViewModel);
            }
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            else
            {
                return View("Index", "Home");
            }  
        }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
