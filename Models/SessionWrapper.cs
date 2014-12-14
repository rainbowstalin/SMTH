using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMTH.Models
{
    public class SessionWrapper
    {
        public static void AbandonSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static Guid UserUuid
        {
            get
            {
                if (HttpContext.Current.Session["UserUuid"] == null)
                    HttpContext.Current.Session["UserUuid"] = Guid.Empty;
                return (Guid)HttpContext.Current.Session["UserUuid"];
            }
            set { HttpContext.Current.Session["UserUuid"] = value; }
        }

        public static Data.RoleE Role
        {
            get
            {
                if (HttpContext.Current.Session["UserUuid"] == null)
                    HttpContext.Current.Session["UserUuid"] = Guid.Empty;
                return (Data.RoleE)HttpContext.Current.Session["UserUuid"];
            }
            set { HttpContext.Current.Session["UserUuid"] = value; }
        }

    }
}