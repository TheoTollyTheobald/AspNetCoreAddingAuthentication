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
            var userResult = _userManager.CreateAsync(user, registerViewModel.Password);
            if (!userResult.Result.Succeeded)
            {
                foreach (var error in userResult.Result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                return View("Register", registerViewModel);
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("HomeController.Index");
            }
            else
            {
                return View(registerViewModel);
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
