using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMTH.Data;

namespace SMTH.ViewModels.Users
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Username field Empty")]
        [StringLength(50, ErrorMessage = "Entry too long")]
        [Display(Name = "User name")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Password field Empty")]
        [StringLength(50, ErrorMessage = "Entry too long")]
        [Display(Name = "User password")]
        public String Password { get; set; }

        [Required(ErrorMessage = "Role Field Empty")]
        [Display(Name = "User role")]
        public RoleE UserRole { get; set; }

        public SelectList UserRoles { get; set; }

        public RegisterUserViewModel()
        {
            UserRoles = new SelectList(Enum.GetNames(typeof(RoleE)));
        }
    }
}