using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Models;

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
    }
}
