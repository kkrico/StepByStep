using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StepByStep.Web.Startup))]
namespace StepByStep.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
