using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Juno.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Category(int id)
        {
            return View();
        }
    }
}