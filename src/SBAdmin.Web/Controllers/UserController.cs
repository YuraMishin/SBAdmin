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
    /// Class UserController.
    /// Implements user management feature
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// UserManager
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager">userManager</param>
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Method displays update profile UI
        /// GET: /user/profile
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Profile()
        {
            var user = _userManager.Users.First(userEntry => userEntry.Email == User.Identity.Name);

            return View(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber
            });
        }

        /// <summary>
        /// Method updates profile
        /// POST: /user/profile
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        [HttpPost]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =
                    _userManager.Users.First(x =>
                        x.Email == User.Identity.Name);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.UserName = model.Email;
                user.NormalizedUserName = model.Email.ToUpper();
                user.PhoneNumber = model.Phone;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ViewData["message"] = "Successfully Updated Profile";
                }
                else
                    ViewData["message"] = "Profile Update Error!";
            }
            return View();
        }
    }
}

