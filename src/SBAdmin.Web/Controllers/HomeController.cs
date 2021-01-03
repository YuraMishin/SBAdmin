using Microsoft.AspNetCore.Mvc;

namespace SBAdmin.Web.Controllers
{
    /// <summary>
    /// Class HomeController.
    /// Implements rooting 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Method displays home page.
        /// GET: /
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
