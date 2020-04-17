using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookStore.Domain.DataInitialize;
using BookStore.Domain.EF;

namespace BookStore.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new CreateDatabaseIfNotExists<BookStoreContext>());
            DataInitializer.InitializeTables();
            DataInitializer.RemoveDuplicates();
        }
    }
}