using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SBAdmin.Web.ViewModels;

namespace SBAdmin.Web.Controllers
{
    /// <summary>
    /// Class RoleController
    /// Implements role management
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        /// <summary>
        /// RoleManager
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<RoleController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleManager">roleManager</param>
        /// <param name="logger">logger</param>
        public RoleController(RoleManager<IdentityRole> roleManager,
            ILogger<RoleController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// Method displays Role UI
        /// GET: /role/
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            var model = _roleManager.Roles.Select(role => new RolesViewModel
            {
                Id = role.Id,
                Name = role.Name
            });

            return View("Index", model);
        }

        /// <summary>
        /// Method displays role details UI
        /// GET: RoleController/Details/5
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Task&lt;ActionResult&gt;</returns>
        public async Task<ActionResult> Details(string name)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(name))
                {
                    ModelState.AddModelError("", "Accepted role with that name doesn't exist.");
                    return RedirectToAction(nameof(Index));
                }
                var result = await _roleManager.FindByNameAsync(name);
                return View(new RolesViewModel
                {
                    Id = result.Id,
                    Name = result.Name
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Method displays create UI
        /// GET: RoleController/Create
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Method handles create role feature
        /// POST: RoleController/Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RolesViewModel model)
        {
            try
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = model.Name
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return View();
            }
        }

        /// <summary>
        /// Method displays edit role UI.
        /// RoleController/Edit/5
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Task&lt;ActionResult&gt;</returns>
        public async Task<ActionResult> Edit(string name)
        {
            var result = await _roleManager.FindByNameAsync(name);
            return View(new RolesViewModel
            {
                Id = result.Id,
                Name = result.Name
            });
        }

        /// <summary>
        /// Method handles edit role feature.
        /// RoleController/Edit/5
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        /// <returns>Task&lt;ActionResult&gt;</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string name)
        {
            try
            {
                if (await _roleManager.RoleExistsAsync(name))
                {
                    ModelState.AddModelError("", "A role with that name already exists!");
                    return View();
                }

                var role = await _roleManager.FindByIdAsync(id);
                role.Name = name;
                role.NormalizedName = name.ToUpper();
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Couldn't update the role.");
                return View();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Method displays delete role UI. 
        /// Get: /RoleController/Delete/5 
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Task&lt;ActionResult&gt;</returns>
        public async Task<ActionResult> Delete(string name)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(name))
                {
                    ModelState.AddModelError("", "Accepted role with that name doesn't exist.");
                    return View();
                }
                var role = await _roleManager.FindByNameAsync(name);
                return View(new RolesViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Method handles delete role feature.
        /// POST: RoleController/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", $"The {role.Name} could not be deleted.");
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
