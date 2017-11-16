using CircleSpaceServiceLib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircleSpace.Controllers
{

    [Authorize]
    public class CreatorController : Controller
    {
        private ICircleSpaceService service = new DummyCircleSpaceService();
        // GET: Creator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateWebPage()
        {
            //Needs a List<Layouts> that will become option to be selected and applied to the live preview Window
            return View(service.GetLayouts());
        }
    }
}