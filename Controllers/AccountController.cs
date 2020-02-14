using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AcadaAcademy.DataModels;
using AcadaAcademy.ViewModels;

namespace LostZone.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AcadaUser> _signInManager;

        public AccountController(SignInManager<AcadaUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Login()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Items", "App");
            }
            return View();
        }

        
        //When user is not already logged in, verify that this is a valid login when the user posts the login information.
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        RedirectToAction("Items", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }
            return View();
        }

        //Logout out signed in user and redirect action to the home page
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }
    }
}
