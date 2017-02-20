using System.Web.Mvc;

namespace StepByStep.Web.Infra.Theme
{
    public class ThemeViewEngine : RazorViewEngine
    {
        public ThemeViewEngine()
        {
            
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var theme = controllerContext.HttpContext.Request.Cookies["Theme"]?.Value ?? Bootstrap.Theme.Default;
            ViewLocationFormats = new[]
            {
                $"~/Theme/" + theme + "/{1}/{0}.cshtml",
                $"~/Theme/" + theme + "/Shared/{0}.cshtml",
            };

            PartialViewLocationFormats = new[]
            {
                $"~/Theme/" + theme + "/{1}/{0}.cshtml",
                $"~/Theme/" + theme + "/Shared/{0}.cshtml",
            };

            AreaViewLocationFormats = new[]
            {
                $"~/Theme/" + theme + "/{1}/{0}.cshtml",
                $"~/Theme/" + theme + "/Shared/{0}.cshtml",
            };

            AreaPartialViewLocationFormats = new[]
            {
                $"~/Theme/" + theme + "/{1}/{0}.cshtml",
                $"~/Theme/" + theme + "/Shared/{0}.cshtml",
            };

            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }
}