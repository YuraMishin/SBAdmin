using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SBAdmin.Web.ViewModels
{
    /// <summary>
    /// Class UserViewModel.
    /// Implements User View Model
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        ///  Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Phone No
        /// </summary>
        [DisplayName("Phone No")]
        public string Phone { get; set; }

        /// <summary>
        /// User in roles
        /// </summary>
        public List<RolesViewModel> UserInRoles { get; set; }

        /// <summary>
        /// Is Locked Out
        /// </summary>
        public bool IsLockedOut { get; set; }
    }
}
