using CircleSpaceGeneralModels.Models;
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
            PageModel p = new PageModel();
            return View(p);
        }
        public ActionResult BrowseSite()
        {
            return View();
        }
    }
}