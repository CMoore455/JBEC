using CircleSpace.Models;
using CircleSpaceGeneralModels.Models;
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
        private ICircleSpaceService service;
        // GET: Creator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateWebPage()
        {
                        //Needs a List<Layouts> that will become option to be selected and applied to the live preview Window
            return
            View(service.GetLayouts());
            //View(new List<LayoutModel>() { new LayoutModel() { ID = 2, LayoutTitle = "Header", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Header },
            //    new LayoutModel() { ID = 3, LayoutTitle = "Body", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Body },
            //    new LayoutModel() { ID = 1, LayoutTitle = "Footer", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Footer } });
        }

        public JsonResult GetNewLayout(int id)
        {
            LayoutModel layout =
                //new LayoutModel() { ID = 1, CSS = "h1 { color: blue; } p {color: green; }", Content = "<h1>Blah</h1><p>ofofo</p>", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Body, LayoutTitle = "Layout1" };
                service.GetLayoutWithID(id);
            return Json(new LayoutModelJSON(layout).JSON, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateCustomLayout()
        {
            return View();
        }

        [HttpPost]
        public string SavePage(JSONForSavingWebPage o)
        {
            PageModel pageModel = new PageModel()
            {
                Header = o.Header,
                Body = o.Body,
                Footer = o.Footer,
                CSS = o.CSS,
                //Need Route
                //Need ImageURLS
            };

            service.AddPage(pageModel);

            return "Success"; 
        }
    }
}
