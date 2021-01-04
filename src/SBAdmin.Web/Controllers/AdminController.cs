using Microsoft.AspNetCore.Mvc;

namespace SBAdmin.Web.Controllers
{
    /// <summary>
    /// Class AdminController.
    /// Implements admin rooting
    /// </summary>
    public class AdminController : Controller
    {
        /// <summary>
        /// Method displays admin home page.
        /// GET: /admin
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
