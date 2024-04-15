using System.Web.Optimization;

namespace FirstProj
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/glightbox/js").Include(
                "~/assets/vendor/glightbox/js/glightbox.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/isotope/js").Include(
                "~/assets/vendor/isotope-layout/isotope.pkgd.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/waypoints/js").Include(
                "~/assets/vendor/waypoints/noframework.waypoints.js"));
            bundles.Add(new ScriptBundle("~/bundles/validate/js").Include(
                "~/assets/vendor/php-email-form/validate.js"));
            bundles.Add(new ScriptBundle("~/bundles/main/js").Include(
                "~/assets/js/main.js"));


            bundles.Add(new StyleBundle("~/bundles/styles/animate/css").Include(
                "~/assets/vendor/animate.css/animate.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/styles/bootstrap/css").Include(
                "~/assets/vendor/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/styles/bootstrap-icon/css").Include(
                "~/assets/vendor/bootstrap-icons/bootstrap-icons.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/styles/boxicons/css").Include(
                "~/assets/vendor/boxicons/css/boxicons.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/styles/glightbox/css").Include(
                "~/assets/vendor/glightbox/css/glightbox.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/styles/remixicon/css").Include(
                "~/assets/vendor/remixicon/remixicon.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/styles/swiper-bundle/css").Include(
                "~/assets/vendor/swiper/swiper-bundle.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/styles/style/css").Include(
                "~/assets/css/style.css", new CssRewriteUrlTransform()));



        }
    }
}