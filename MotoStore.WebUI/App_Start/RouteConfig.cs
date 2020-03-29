using System.Web.Mvc;
using System.Web.Routing;

namespace MotoStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "",
                "Products/{action}/{makeForList}/{id}", //{controller}
                new {controller = "Products", action = "Motorcycles", id = 0, makeForList = "All"}
            );
            routes.MapRoute(
                "",
                "Account/{action}", //{controller}
                new {controller = "Account"}
            );
            routes.MapRoute(
                "",
                "Order/{action}", //{controller}
                new {controller = "Order"}
            );
            routes.MapRoute(
                "",
                "Shop/{action}", //{controller}
                new {controller = "Shop"}
            );
            routes.MapRoute(
                "",
                "Admin/{action}", //{controller}
                new {controller = "Admin"}
            );
            // routes.MapRoute(
            //    name: "",
            //    url: "{controller}/{action}",//{controller}
            //    defaults: new { controller = "Home", action="Index" }
            //);
            routes.MapRoute(
                "",
                "Help/{action}", //{controller}
                new {controller = "Help"}
            );
            routes.MapRoute(
                "",
                "{*url}", //{controller}{controller}/{action}
                new {controller = "Home", action = "Index"}
            );
        }
    }
}