using StepByStep.Web.Infra.Theme;
using StepByStep.Web.Infra.ValueProviders;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StepByStep.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Insert(0, new ThemeViewEngine());
            ValueProviderFactories.Factories.Insert(0, new CookieValueProviderFactory());
        }
    }
}
