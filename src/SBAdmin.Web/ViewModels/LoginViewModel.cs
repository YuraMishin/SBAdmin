using System.ComponentModel.DataAnnotations;

namespace SBAdmin.Web.ViewModels
{
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

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
