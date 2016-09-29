using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace YokyDoky.Filters
{

    public enum UserType
    {
        Admin = 1,
        User = 2
    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        public new UserType Roles;  // new keyword will hide base class Roles Property
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                //throw new ArgumentNullException("httpContext");
                httpContext.RewritePath("Home/Index");
            }

            if (null == httpContext.Session[YokyDoky.claGlobal.SessionString_UserType]
                || "" == httpContext.Session[YokyDoky.claGlobal.SessionString_UserType].ToString())
            {
                return false;
            }

            UserType role
                = (UserType)Convert.ToInt32(httpContext.Session[YokyDoky.claGlobal.SessionString_UserType]);

            // you could get User role or user type from session.

            if (Roles != 0 && ((Roles & role) != role))
            {
                return false;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/SignIn");
        }
    }
}