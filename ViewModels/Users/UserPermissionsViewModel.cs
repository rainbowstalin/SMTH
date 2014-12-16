using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMTH.Data;
using SMTH.Data.Users;
using DA = SMTH.Data.Users.DataAccess;

namespace SMTH.ViewModels.Users
{
    public class UserPermissionsViewModel
    {
        [Display(Name = "Role")]
        public RoleE? Role { get; set; }

        public SelectList Roles { get; set; }

        public List<RolePermissionDto> RolePermissions { get; set; }

        public UserPermissionsViewModel()
        {
            init();
        }

        public UserPermissionsViewModel(RoleE role)
        {
            Role = role;
            RolePermissions = DA.GetRolePermissionList(role);
            init();
        }

        void init()
        {
            Roles = new SelectList(Enum.GetNames(typeof(RoleE)));
        }
    }
}