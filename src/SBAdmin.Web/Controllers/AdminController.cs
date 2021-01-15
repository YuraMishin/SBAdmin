using System.Threading.Tasks;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager">UserManager</param>
        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        /// <summary>
        /// Method displays admin home page.
        /// GET: /admin
        /// </summary>
        /// <returns></returns>
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
    }
}
