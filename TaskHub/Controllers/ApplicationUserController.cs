using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Models;
using TaskHub.Models.ModelsView;

namespace TaskHub.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LogInView model)
        {
            if (ModelState.IsValid)
            {
                //Uses the default SignInManager to sign in if the user passes the authentication.
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid Login attempt");
            }

            return View();
        }
    }
}
