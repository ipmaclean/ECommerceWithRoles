using System.Web;
using System.Web.Optimization;

namespace ECommerceWithRoles
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.6.0.js",
                        "~/Scripts/jquery.easing.1.3.min.js",
                        "~/Scripts/jquery.sticky.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/main.js",
                        "~/Scripts/bxslider.min.js",
                        "~/Scripts/script.slider.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/animate.min.css",
                      "~/Content/site.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/owl.theme.default.min.css",
                      "~/Content/style.css",
                      "~/Content/responsive.css"
                      ));
        }
    }
}
