using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMTH.Data.Users
{

    public class UserDto
    {
        public Guid UserId { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public RoleE UserRole { get; set; }
    }

    public class UserPermission
    {
        public Guid UserPermissionId { get; set; }
        public String PermissionName { get; set; }
        public String PermissionDescription { get; set; }
    }

    public class DataAccess
    {
        public static UserDto GetUser(String userName)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                return (from u in db.Users
                        join r in db.Roles on u.UserRoleUuid equals r.RoleUuid
                        where u.UserName == userName
                        select new UserDto
                        { 
                            UserId = u.UserUuid,
                            Password = u.Password,
                            UserName = u.UserName,
                            UserRole = (RoleE)Enum.Parse(typeof(RoleE), r.Rolename, true)
                        }).FirstOrDefault();
            }
        }

        public static bool ValidateUser(String userName, String password)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                return (from u in db.Users
                        where u.UserName == userName
                        where u.Password == password
                        select new Object()).Any();
            }
        }
        
        public static bool UserHasPermission(Guid userUuid, PermissionE permission)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                return (from rp in db.RolePermissions
                            join u in db.Users on rp.RoleUuid equals u.UserRoleUuid
                            join p in db.Permissions on rp.PermissionUuid equals p.PersimissionUuid
                            where u.UserUuid == userUuid
                            where p.PersimissionName == Enum.GetName(typeof(PermissionE), permission)
                            select new Object()).Any();
            }
        }

        public static Guid GetRoleUuid(RoleE role)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                return (from r in db.Roles
                        where r.Rolename == Enum.GetName(typeof(RoleE), role)
                        select r.RoleUuid).FirstOrDefault();
            }
        }

        public static void AddUser(UserDto newUser)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                db.Users.InsertOnSubmit(new User
                {
                    UserUuid = newUser.UserId,
                    UserName = newUser.UserName,
                    Password = newUser.Password,
                    UserRoleUuid = GetRoleUuid(newUser.UserRole)
                });
                db.SubmitChanges();
            }
        }

    }
}