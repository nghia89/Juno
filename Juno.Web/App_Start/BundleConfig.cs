using System.Web;
using System.Web.Optimization;

namespace Juno.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Assets/Client/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                "~/Assets/Admin/libs/jquery-ui/jquery-ui.min.js",
                "~/Assets/Admin/libs/mustache/mustache.js",
                "~/Assets/Admin/libs/numeral/numeral.js",
                "~/Assets/Client/js/controller/common.js",
                "~/Assets/Admin/libs/jquery-validation/dist/jquery.validate.min.js",
                "~/Assets/Admin/libs/jquery-validation/dist/additional-methods.min.js"
                ));
            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Assets/Client/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Assets/Admin/libs/jquery-ui/themes/smoothness/jquery-ui.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/Client/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Assets/Client/css/StyleSheetCustom.css", new CssRewriteUrlTransform())
                );
            BundleTable.EnableOptimizations = true;
        }
    }
}
