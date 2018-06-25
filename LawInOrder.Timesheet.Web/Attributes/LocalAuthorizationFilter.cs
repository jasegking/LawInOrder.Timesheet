using System;
using System.Web;
using System.Web.Mvc;

namespace LawInOrder.Timesheet.Web.Attributes
{
    public class LocalAuthorizationFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            object userId = httpContext.Session["LoggedInUserId"];
            if (userId != null)
            {
                return true;
            }

            return base.AuthorizeCore(httpContext);
        }

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    // Return a "403 Forbidden" status rather than redirecting to the login page with the standard "401 Unauthorized"
        //    filterContext.Result = new HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
        //}
    }
}