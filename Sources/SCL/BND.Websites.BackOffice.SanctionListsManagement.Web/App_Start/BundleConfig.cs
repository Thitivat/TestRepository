using System.Web.Optimization;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit <a href="http://go.microsoft.com/fwlink/?LinkId=301862">Bundling and Minification</a>
        /// <summary>
        /// Registers the bundles to the bundle table when application start.
        /// </summary>
        /// <param name="bundles">BundleCollection object.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jsframeworks").Include(
                        "~/Scripts/jquery-{version}.js"
                        //,
                        //"~/Scripts/angular.min.js",
                        //"~/Scripts/ui-bootstrap-tpls-0.13.3.min.js",
                        //"~/Scripts/angular-animate.min.js",
                        //"~/Scripts/angular-messages.min.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/console").Include(
            //"~/Scripts/kyc/common.js"
            //));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //            "~/Scripts/bootstrap.min.js"));

            BundleTable.EnableOptimizations = true;

            //TODO:: fix Bundle issue
            //bundles.Add(new StyleBundle("~/Content/Css").Include(
            //            "~/Content/Css/Bootstrap/bootstrap.css",
            //            "~/Content/Css/Common.css",
            //            "~/Content/Css/fonts"));
        }
    }
}