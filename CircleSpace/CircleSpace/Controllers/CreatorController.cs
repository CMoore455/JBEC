using CircleSpaceGeneralModels.Models;
using CircleSpaceServiceLib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircleSpace.Controllers
{

    //[Authorize]
    public class CreatorController : Controller
    {
        private ICircleSpaceService service;
        // GET: Creator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateWebPage()
        {
            //Needs a List<Layouts> that will become option to be selected and applied to the live preview Window
            return View(new List<LayoutModel>() { new LayoutModel() { ID = 2, LayoutTitle = "Header", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Header },
                new LayoutModel() { ID = 3, LayoutTitle = "Body", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Body },
                new LayoutModel() { ID = 1, LayoutTitle = "Footer", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Footer } });//service.GetLayouts());
        }
    }
}