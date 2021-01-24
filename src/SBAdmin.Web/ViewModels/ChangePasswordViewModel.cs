using System.ComponentModel.DataAnnotations;

namespace SBAdmin.Web.ViewModels
{
    /// <summary>
    /// Class ChangePasswordViewModel.
    /// Implements Change Password View Model
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Old password
        /// </summary>
        [Required, DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        [Display(Name = "New password")]
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm New Password
        /// </summary>
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
