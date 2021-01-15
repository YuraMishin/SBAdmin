using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SBAdmin.Web.Models
{
    /// <summary>
    /// Class User.
    /// Implements User Entity
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// First Name
        /// </summary>
        [Required]
        [MaxLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        [MaxLength(25)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
