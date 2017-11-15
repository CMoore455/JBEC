using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircleSpace.Controllers
{
    [AllowAnonymous]
    public class BrowseController : Controller
    {
        // GET: Browse
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BrowseSite()
        {
            return View();
        }
    }
}