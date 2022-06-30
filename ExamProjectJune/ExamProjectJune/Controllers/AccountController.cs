using ExamProjectJune.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectJune.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;

        }
        
        public async Task<IActionResult> Register()
        {
            IdentityUser newUser = new IdentityUser()
            {
                Email = "admin@code.az",
                UserName = "admin"
            };

            IdentityResult result = await userManager.CreateAsync(newUser, "Asdasd12");
            if (!result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return Content("Not Okey");
            }
            return Content("Okey");
        }

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            IdentityUser loggingUser = await userManager.FindByEmailAsync(viewModel.Email);
            if (loggingUser == null)
            {
                ModelState.AddModelError("", "Email or Password is wrong");
                return View(viewModel);
                
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(loggingUser, viewModel.Password, viewModel.StayConnected, false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("result", "You are locked out, Try after 30 minutes");
                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError("result", "Email or Password is wrong");
                    return View(viewModel);
                }
            }

            return RedirectToAction("index", "home", new {area="admin" });
        }


        
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
    } 
}
