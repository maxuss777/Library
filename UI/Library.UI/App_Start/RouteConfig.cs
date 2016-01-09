using System.Web.Mvc;
using System.Web.Routing;

namespace Library.UI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Books",
                    action = "AllBooksList",
                    category = 0,
                    page = 1
                });

            routes.MapRoute(null,
                "Page{page}",
                new {controller = "Books", action = "AllBooksList", page = 1},
                new {page = @"\d+"});

            routes.MapRoute(null,
                "{categoryId}/Page{page}",
                new { controller = "Books", action = "AllBooksList"},
                new { page=@"\d+"});

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
