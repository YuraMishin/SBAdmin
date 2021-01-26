using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        /// ILogger
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// RoleManager
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager">userManager</param>
        /// <param name="logger">logger</param>
        /// <param name="roleManager">roleManager</param>
        public UserController(
            UserManager<User> userManager,
            ILogger<UserController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;

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
        [Authorize(Roles = "User")]
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

        /// <summary>
        /// Method displays change password UI.
        /// GET: /user/changepassword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Method handles change password.
        /// POST: /user/changepassword
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    ModelState.AddModelError("", "Password could not be changed due to error.");
                    return View();
                }
                ModelState.AddModelError("", "Password could not be changed due to error.");
                return View();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("", "Password could not be changed due to error.");
                return View();
            }
        }

        /// <summary>
        /// Method displays list of users.
        /// GET: /user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var users = _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });

            return View(users);
        }

        /// <summary>
        /// Method displays user details UI.
        /// GET: User/Details/5
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Task&lt;ActionResult&gt;</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            var roles = _roleManager.Roles.ToList();
            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            var model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                UserInRoles = roles.Select(x => new RolesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Selected = userRoles.Exists(y => y == x.Name)
                }).ToList()
            };
            return View(model);
        }

        /// <summary>
        /// Method handles user details.
        /// /// POST: User/Details/5
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="model">model</param>
        /// <returns>Task&lt;ActionResult&gt;</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id, UserViewModel model)
        {
            var user = _userManager.Users.First(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();
            foreach (var item in model.UserInRoles)
            {
                if (item.Selected)
                {
                    await _userManager.AddToRoleAsync(user, roles.First(x => x.Id == item.Id).Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roles.First(x => x.Id == item.Id).Name);
                }
            }
            return RedirectToAction("Details", new { id });
        }

        /// <summary>
        /// Method handles delete user.
        /// GET: User/Delete/5
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Task&lt;ActionResult&gt;</returns>
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var user = _userManager.Users.First(x => x.Id == id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}

