using System.Web.Mvc;
using Library.UI.Filters;

namespace Library.UI.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MyAuthorizeAttribute());
        }
    }
}