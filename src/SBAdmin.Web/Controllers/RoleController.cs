using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBAdmin.Web.ViewModels;

namespace SBAdmin.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: RoleController
        public ActionResult Index()
        {
            var model = _roleManager.Roles.Select(role => new RolesViewModel
            {
                Id = role.Id,
                Name = role.Name
            });

            return View("Index", model);
        }

        // GET: RoleController/Details/5
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
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
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
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public async Task<ActionResult> Edit(string name)
        {
            var result = await _roleManager.FindByNameAsync(name);
            return View(new RolesViewModel
            {
                Id = result.Id,
                Name = result.Name
            });
        }

        // POST: RoleController/Edit/5
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
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: RoleController/Delete/5
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
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RoleController/Delete/5
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
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
