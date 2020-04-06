using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MotoStore.Domain.DataInitialize;
using MotoStore.Domain.EF;

namespace MotoStore.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new CreateDatabaseIfNotExists<MotoStoreContext>());
            DataInitializer.InitializeTables();
            DataInitializer.RemoveDuplicates();
        }
    }
}