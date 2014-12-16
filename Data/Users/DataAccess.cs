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

    public class RolePermissionDto
    {
        public Guid PermissionId { get; set; }
        public String PermissionName { get; set; }
        public bool isTrue { get; set; }
    }

    public class DataAccess
    {
        public static void LogLogin(Guid loginId, Guid userId)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                db.Logins.InsertOnSubmit(new Login {
                    LoginUuid = loginId,
                    LoginTime = DateTime.Now,
                    UserUuid = userId
                });
                db.SubmitChanges();
            }
        }

        public static void LogLogin(Guid loginId)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                var login = (from l in db.Logins
                             where l.LoginUuid == loginId
                             select l).FirstOrDefault();
                login.LogoffTime = DateTime.Now;
                db.SubmitChanges();
            }
        }

        public static List<RolePermissionDto> GetRolePermissionList(RoleE role)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                var roleUuid = GetRoleUuid(role);

                var pList = (from p in db.Permissions
                    select new RolePermissionDto
                    {
                        PermissionId = p.PersimissionUuid,
                        PermissionName = p.PersimissionName,
                        isTrue = false
                    }).ToList();
                foreach(var p in pList)
                {
                    if (db.RolePermissions.Where(x => x.RoleUuid == roleUuid).Any(x => x.PermissionUuid == p.PermissionId))
                        p.isTrue = true;
                }
                return pList;
            }
        }

        public static void SetRolePermissions(RoleE role, IEnumerable<RolePermissionDto> rolePermissions)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                var roleUuid = GetRoleUuid(role);

                foreach(var r in rolePermissions)
                {
                    var rolePermission = (from rp in db.RolePermissions
                                          where rp.PermissionUuid == r.PermissionId
                                          where rp.RoleUuid == roleUuid
                                          select rp).FirstOrDefault();

                    if (!r.isTrue && rolePermission != null)
                    {
                        db.RolePermissions.DeleteOnSubmit(rolePermission);
                    }

                    if (r.isTrue && rolePermission == null)
                    {
                        db.RolePermissions.InsertOnSubmit(new RolePermission
                        {
                            RolePermissionUuid = Guid.NewGuid(),
                            RoleUuid = roleUuid,
                            PermissionUuid = r.PermissionId
                        });
                    }
                }
                
                db.SubmitChanges();
            }
        }

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

        public static bool RoleHasPermission(RoleE role, Guid permission)
        {
            using (UsersViewDataContext db = new UsersViewDataContext())
            {
                return (from rp in db.RolePermissions
                        join r in db.Roles on rp.RoleUuid equals  r.RoleUuid
                        join p in db.Permissions on rp.PermissionUuid equals p.PersimissionUuid
                        where r.Rolename == Enum.GetName(typeof(RoleE), role)
                        where p.PersimissionUuid == permission
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