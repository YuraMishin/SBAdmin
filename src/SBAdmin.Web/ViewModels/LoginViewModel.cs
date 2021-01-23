using System.ComponentModel.DataAnnotations;

namespace SBAdmin.Web.ViewModels
{
    /// <summary>
    /// Class LoginViewModel.
    /// Implements Login View Model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// User Name
        /// </summary>
        [Display(Name = "Email")]
        [Required, DataType(DataType.EmailAddress), MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Display(Name = "Password")]
        [Required, DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Remember Me
        /// </summary>
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
