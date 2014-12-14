using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMTH.Models
{
    public class AuthorizeUser : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated || SessionWrapper.UserUuid == Guid.Empty || SessionWrapper.UserUuid == null)
                return false;

            return true;
        }
    }
}