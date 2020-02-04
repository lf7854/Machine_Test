using System.Web;
using System.Web.Optimization;

namespace PracticeMVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.data-tables.min.js",
                        "~/Scripts/jquery.touch-swipe.min.js",
                        "~/Scripts/navbar-dropdown.js",
                        "~/Scripts/nav-dropdown.js",
                        "~/Scripts/popper.min.js",
                        "~/Scripts/smooth-scroll.js",
                        "~/Scripts/tabs.js",
                        "~/Scripts/tether.min.js",
                        "~/Scripts/data-tables.bootstrap4.min.js",
                        "~/Scripts/theme.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-grid.min.css",
                      "~/Content/bootstrap-reboot.min.css",
                      "~/Content/data-tables.bootstrap4.min.css",
                      "~/Content/dropdown.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/icons.css",
                      "~/Content/site.css",
                      "~/Content/tether.min.css",
                      "~/Content/theme.css",
                      "~/Content/ajaxLoader.css"));
        }
    }
}
