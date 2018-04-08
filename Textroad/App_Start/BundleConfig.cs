using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;
using System.Web;
using System.Web.Optimization;

namespace Textroad
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //default
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

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


            bundles.UseCdn = true;

            var nullBuilder = new NullBuilder();
            var styleTransformer = new StyleTransformer();
            var scriptTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();


            //Style Bundles
            var bootstrapStylesBundle = new Bundle("~/css/bootstrap");
            bootstrapStylesBundle.Include(
                "~/Content/css/bootstrap.min.css");
            bootstrapStylesBundle.Builder = nullBuilder;
            bootstrapStylesBundle.Transforms.Add(styleTransformer);
            bootstrapStylesBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapStylesBundle);

            var bootstrapRtlStylesBundle = new Bundle("~/css/bootstrap-rtl");
            bootstrapRtlStylesBundle.Include(
                "~/Content/css/bootstrap-rtl.min.css");
            bootstrapRtlStylesBundle.Builder = nullBuilder;
            bootstrapRtlStylesBundle.Transforms.Add(styleTransformer);
            bootstrapRtlStylesBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapRtlStylesBundle);

            var beyondStylesBundle = new Bundle("~/css/beyond");
            beyondStylesBundle.Include(
                "~/Content/css/beyond.min.css",
                "~/Content/css/demo.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/typicons.min.css",
                "~/Content/css/weather-icons.min.css",
                "~/Content/css/animate.min.css");
            beyondStylesBundle.Builder = nullBuilder;
            beyondStylesBundle.Transforms.Add(styleTransformer);
            beyondStylesBundle.Orderer = nullOrderer;
            bundles.Add(beyondStylesBundle);

            var beyondRtlStylesBundle = new Bundle("~/css/beyond-rtl");
            beyondRtlStylesBundle.Include(
                "~/Content/css/beyond-rtl.min.css",
                "~/Content/css/demo.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/typicons.min.css",
                "~/Content/css/weather-icons.min.css",
                "~/Content/css/animate.min.css");
            beyondRtlStylesBundle.Builder = nullBuilder;
            beyondRtlStylesBundle.Transforms.Add(styleTransformer);
            beyondRtlStylesBundle.Orderer = nullOrderer;
            bundles.Add(beyondRtlStylesBundle);


            //Script Bunldes
            var skinBundle = new Bundle("~/bundles/skin");
            skinBundle.Include("~/Content/js/skins.min.js");
            skinBundle.Builder = nullBuilder;
            skinBundle.Transforms.Add(scriptTransformer);
            skinBundle.Orderer = nullOrderer;
            bundles.Add(skinBundle);

            var jQueryBundle = new Bundle("~/bundles/jquery");
            jQueryBundle.Include("~/Content/js/jquery.min.js");
            jQueryBundle.Builder = nullBuilder;
            jQueryBundle.Transforms.Add(scriptTransformer);
            jQueryBundle.Orderer = nullOrderer;
            bundles.Add(jQueryBundle);

            var bootstrapBundle = new Bundle("~/bundles/bootstrap");
            bootstrapBundle.Include(
                  "~/Content/js/bootstrap.min.js",
                "~/Content/js/slimscroll/jquery.slimscroll.min.js");
            bootstrapBundle.Builder = nullBuilder;
            bootstrapBundle.Transforms.Add(scriptTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            var beyondBundle = new Bundle("~/bundles/beyond");
            beyondBundle.Include(
                  "~/Content/js/beyond.min.js");
            beyondBundle.Builder = nullBuilder;
            beyondBundle.Transforms.Add(scriptTransformer);
            beyondBundle.Orderer = nullOrderer;
            bundles.Add(beyondBundle);


            var jQueryValBundle = new Bundle("~/bundles/jqueryval");
            jQueryValBundle.Include(
                 "~/Content/js/jqueryval/jquery.validate*");
            jQueryValBundle.Builder = nullBuilder;
            jQueryValBundle.Transforms.Add(scriptTransformer);
            jQueryValBundle.Orderer = nullOrderer;
            bundles.Add(jQueryValBundle);
        }
    }
}
