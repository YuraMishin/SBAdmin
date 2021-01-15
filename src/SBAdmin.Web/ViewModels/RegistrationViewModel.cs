using System.ComponentModel.DataAnnotations;

namespace SBAdmin.Web.ViewModels
{
    /// <summary>
    /// Class RegistrationViewModel.
    /// Implements Registration View Model
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// User Name
        /// </summary>
        [Display(Name = "Email")]
        [Required, DataType(DataType.Text), MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Display(Name = "Password")]
        [Required, DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [Display(Name = "Confirm Password")]
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [Display(Name = "First Name")]
        [Required, DataType(DataType.Text), MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        [Display(Name = "Last Name")]
        [Required, DataType(DataType.Text), MaxLength(50)]
        public string LastName { get; set; }
    }
}
