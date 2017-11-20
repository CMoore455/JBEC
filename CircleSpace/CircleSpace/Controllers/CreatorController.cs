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
            //Needs a List<LayoutModel> that will become option to be selected and applied to the live preview Window
            //  return View(service.GetLayouts());
            return View(/*new List<LayoutModel>() { new LayoutModel() { ID = 1, CSS = "h1 { color: blue; }", Content = "<h1>Blah</h1>", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Body, LayoutTitle = "Layout1" } });*/
                service.GetLayouts());
        }

        public JsonResult GetNewLayout(int id)
        {
            LayoutModel layout = /*new LayoutModel() { ID = 1, CSS = "h1 { color: blue; } p {color: green; }", Content = "<h1>Blah</h1><p>ofofo</p>", Type = CircleSpaceGeneralModels.Enums.LayoutTypes.Body, LayoutTitle = "Layout1" };*/
                service.GetLayoutWithID(id);
            return Json(new LayoutModelJSON(layout).JSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SavePage(object o)
        {
            return null; 
        }
    }
}