using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CustomInfra.DataBase.Simple.DbContext;
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

            IoCInfra.Container.Register<IDbInfraContext, DbContextInfra>(IoCInfraLifeCycle.Scoped, ConnectionString.TfsConnection);
            
            IoCInfra.StartAttributeRegistration();
        }
    }
}
