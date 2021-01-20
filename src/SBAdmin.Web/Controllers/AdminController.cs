using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBAdmin.Web.Models;
using SBAdmin.Web.ViewModels;

namespace SBAdmin.Web.Controllers
{
    /// <summary>
    /// Class AdminController.
    /// Implements admin rooting
    /// </summary>
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager">UserManager</param>
        public AdminController(
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        /// <summary>
        /// Method displays admin home page.
        /// GET: /admin
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// Method displays registration page.
        /// GET: /register
        /// </summary>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        /// <summary>
        /// Method handles registration feature.
        /// POST: /register
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError("", error.Description);
                }

            }

            return View("Register");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                        model.UserName,
                        model.Password,
                        model.RememberMe,
                        false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].FirstOrDefault());
                    }

                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Failed to Login");
            }


            return View("Login");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
