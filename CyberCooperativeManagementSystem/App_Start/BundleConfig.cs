using System.Web;
using System.Web.Optimization;

namespace CyberCooperativeManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
                
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                         //"~/Scripts/tether.js",
                        "~/Scripts/bootstrap.min.js",
                         
                        "~/Scripts/respond.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/jquery.jeditable.js",
                        "~/Scripts/jquery-ui-1.12.1.js",
                         "~/Scripts/jquery.validate.js",
                         "~/Scripts/jqdt/media/js/jquery.dataTables.editable.js",
                         "~/Scripts/angular.js",
                         "~/Scripts/myscripts/EmployeeScripts.js",
                          "~/Content/select2/src/js/jquery.select2.js",
                         "~/Content/select2/dist/js/select2.js",
                        "~/Scripts/datatables/datatables.bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                     "~/Scripts/myscripts/Module.js",
                      "~/Scripts/myscripts/Controller.js",
                       "~/Scripts/myscripts/Service.js",
                        "~/Scripts/myscripts/EmployeeScripts.js"
                
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/Views/Shared").Include(
                        "~/Views/Shared/frontend/js/jquery-1.10.2.min.js",

                        "~/Views/Shared/frontend/js/bootstrap.min.js",

                        "~/Views/Shared/frontend/js/core.js"
                         ));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          //"~/Scripts/bootstrap.js",
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                       "~/Content/bootstrapunited.css",
                        "~/Content/mystle.css",
                          "~/Content/PagedList.css.css",
                           "~/Content/ui-bootstrap-csp.css",
                       "~/Scripts/jqdt/media/css/demo_table.css",
                       "~/Scripts/jqdt/media/css/demo_table_jui.css",
                        "~/Content/themes/base/jquery-ui.css",
                         "~/Scripts/jqdt/media/css/themes/smoothness/jquery-ui-1.7.2.custom.css",
                         "~/Content/select2/dist/css/select2.css",
                       "~/Content/Datatables/css/datatables.bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Views/Shared").Include(
                       "~/Views/Shared/frontend/bootstrap/css/bootstrap.css",
                       "~/Views/Shared/frontend/css/font-awesome.min.css",
                       "~/Views/Shared/frontend/css/style.css"));
        }
    }
}
