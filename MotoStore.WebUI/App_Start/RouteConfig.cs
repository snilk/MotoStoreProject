using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                name: "",
                url: "Products/{action}/{makeForList}/{id}",//{controller}
                defaults: new { controller = "Products", action = "Motorcycles",id=0,makeForList="All"}
            );
            routes.MapRoute(
                name: "",
                url: "Account/{action}",//{controller}
                defaults: new { controller = "Account" }
            );
            routes.MapRoute(
              name: "",
              url: "Order/{action}",//{controller}
              defaults: new { controller = "Order" }
          );
            routes.MapRoute(
             name: "",
             url: "Shop/{action}",//{controller}
             defaults: new { controller = "Shop" }
         );
            routes.MapRoute(
            name: "",
            url: "Admin/{action}",//{controller}
            defaults: new { controller = "Admin" }
        );
            // routes.MapRoute(
            //    name: "",
            //    url: "{controller}/{action}",//{controller}
            //    defaults: new { controller = "Home", action="Index" }
            //);
            routes.MapRoute(
                 name: "",
                 url: "Help/{action}",//{controller}
                 defaults: new { controller = "Help" }
             );
            routes.MapRoute(
            name: "",
            url: "{*url}",//{controller}{controller}/{action}
            defaults: new { controller = "Home", action = "Index" }
        );
        }
    }

    }

