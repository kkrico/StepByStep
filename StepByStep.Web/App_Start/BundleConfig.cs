using StepByStep.Web.Infra.Theme;
using System.Collections.Generic;
using System.Web.Optimization;

namespace StepByStep.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            foreach (var theme in Bootstrap.Themes)
            {
                var stylePath = $"~/Theme/{theme}/bootstrap.css";

                bundles.Add(new StyleBundle(Bootstrap.Bundle(theme))
                    .Include(stylePath));
            }

            var bundleOrder = new NonOrderingBundleOrderer();
            var cssBundle = new StyleBundle("~/css").Include(
                                            "~/Theme/site.css");
            cssBundle.Orderer = bundleOrder;
            bundles.Add(cssBundle);
        }
    }

    public class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
