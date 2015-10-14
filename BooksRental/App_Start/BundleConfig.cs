using System.Web;
using System.Web.Optimization;

namespace BooksRental
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/Main/Global.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Content/Plugins/datatables/media/js/jquery.dataTables.min.js",
                        "~/Content/Plugins/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/Datatable/css").Include(
                      "~/Content/Plugins/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css",
                      "~/Content/Plugins//datatables-responsive/css/dataTables.responsive.css"));
            



        }
    }
}
