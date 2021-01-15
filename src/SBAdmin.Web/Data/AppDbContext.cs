using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SBAdmin.Web.Models;


namespace SBAdmin.Web.Data
{
    /// <summary>
    /// Class implements AppDbContext
    /// </summary>
    public class AppDbContext : IdentityDbContext<User>
    {
        #region DB Tables

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">AppContext</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
    }
}
