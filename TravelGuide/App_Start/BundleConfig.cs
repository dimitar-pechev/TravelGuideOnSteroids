﻿using System.Web.Optimization;

namespace TravelGuide
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js", "~/Scripts/toastr-options.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                        "~/Scripts/materialize/materialize.js"));

            bundles.Add(new ScriptBundle("~/bundles/gallery").Include(
                        "~/Scripts/app/gallery-main-page.js"));

            bundles.Add(new ScriptBundle("~/bundles/store-list").Include(
                        "~/Scripts/app/store-list.js"));

            bundles.Add(new ScriptBundle("~/bundles/store-details").Include(
                        "~/Scripts/app/store-details.js"));

            bundles.Add(new ScriptBundle("~/bundles/articles-list").Include(
                        "~/Scripts/app/articles-list.js"));

            bundles.Add(new ScriptBundle("~/bundles/cart").Include(
                        "~/Scripts/app/cart.js"));

            bundles.Add(new ScriptBundle("~/bundles/story-details").Include(
                        "~/Scripts/app/story-details.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/materialize/css/materialize.css",
                        "~/Content/toastr.css",
                        "~/Content/site.css"));
        }
    }
}
