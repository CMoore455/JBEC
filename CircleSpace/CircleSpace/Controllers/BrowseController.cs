using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircleSpaceServiceLib;
using CircleSpaceServiceLib.Service;
using CricleSpaceGeneralModels.Models;

namespace CircleSpace.Controllers
{
    [AllowAnonymous]
    public class BrowseController : Controller
    {
        ICircleSpaceService service = new StaticCircleSpaceService();
        // GET: Browse
        public ActionResult Index()
        {
            PageModel p = new PageModel();
            return View(p);
        }
        public ActionResult BrowseSite()
        {
            List<PageModel> model = service.GetPages();

            return View();
        }
    }
}