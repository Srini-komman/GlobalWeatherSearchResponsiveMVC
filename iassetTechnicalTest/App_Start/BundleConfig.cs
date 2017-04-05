using System.Web;
using System.Web.Optimization;

namespace iassetTechnicalTest
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/globalweatherapp.js")
                      .IncludeDirectory("~/Scripts/Controllers", "*.js")
                      .IncludeDirectory("~/Scripts/directives", "*.js")
                      .IncludeDirectory("~/Scripts/factories", "*.js"));
        }
    }
}
