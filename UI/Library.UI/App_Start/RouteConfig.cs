using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Library.UI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                "Default",
                "{controller}/{category}/{id}",
                new {controller = "Books", action = "GetAll", category = UrlParameter.Optional,id = UrlParameter.Optional});*/

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Books",
                    action = "GetAll",
                    page = 1
                });
           
            routes.MapRoute(null,
                "Page{page}",
                new {controller = "Books", action = "GetAll", page = 1},
                new {page = @"\d+"});
            
            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Books", action = "GetFiltered"},
                new { page=@"\d+"});

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
