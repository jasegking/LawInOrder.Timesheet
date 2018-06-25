using LawInOrder.Timesheet.Web.Attributes;
using System.Web.Mvc;

namespace LawInOrder.Timesheet.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new LocalAuthorizationFilter());
        }
    }
}
