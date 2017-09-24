using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CustomInfra.DataBase.Simple.Configuration;
using CustomInfra.Injector.Simple.IoC;

namespace TfsGamified.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            IoCInfra.StartAttributeRegistration();
            DbInfra.StartDbContextConfiguration(ConnectionString.TfsConnection);
        }
    }
}
