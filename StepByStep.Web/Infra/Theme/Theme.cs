using System.Collections.Generic;

namespace StepByStep.Web.Infra.Theme
{
    public class Bootstrap
    {
        public const string BundleBase = "~/Theme/";

        public class Theme
        {
            public const string Cerulean = "Cerulean";
            public const string Cosmo = "Cosmo";
            public const string Cyborg = "Cyborg";
            public const string Darkly = "Darkly";
            public const string Default = "Default";
        }

        public static IEnumerable<string> Themes = new HashSet<string>
        {
            Theme.Cerulean,
            Theme.Cosmo,
            Theme.Cyborg,
            Theme.Darkly,
            Theme.Default,
        };

        public static string Bundle(string themename)
        {
            return $"{BundleBase}+{themename}";
        }
    }
}