using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Method OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>()
                .HasData(new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                });
        }
    }
}
