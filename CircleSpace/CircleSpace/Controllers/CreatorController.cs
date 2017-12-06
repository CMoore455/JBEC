using CircleSpace.Models;
using CircleSpaceGeneralModels.Models;
using CircleSpaceServiceLib.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircleSpace.Controllers
{

    [Authorize]
    public class CreatorController : Controller
    {
        private ICircleSpaceService service = new SqlCricleSpaceService();
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

        [HttpGet]
        public ActionResult NameWebsitePage()
        {
            return View();
        }


        [HttpPost]
        public ActionResult NameWebsitePagePost(string route)
        {
            List<string> imgurls = new List<string>();
            HttpFileCollectionBase hfc = Request.Files;
            foreach (string h in hfc)
            {
                HttpPostedFileBase f = hfc[h];
                bool repeat = false;
                string extenstion = Path.GetExtension(f.FileName);
                string newFileName = "";
                do
                {
                    newFileName = Path.GetRandomFileName() + extenstion;
                    if (System.IO.File.Exists(Server.MapPath("~/Content/Images") + "/" + newFileName))
                    {
                        repeat = true;
                    }
                } while (repeat);
                f.SaveAs(Path.Combine(Server.MapPath("~/Content/Images"), newFileName));
                imgurls.Add(newFileName);
            };
            foreach(char c in route)
            {
                route.Replace(' ', '_');
            }
            JSONForSavingWebPage o = new JSONForSavingWebPage()
            {
                Header = "",
                Body = "",
                Footer = "",
                CSS = "",
                Route = route,
                ImageURLS = imgurls
            };
        
            SavePage(o);
            return Redirect($"~/Controllers/Creator/EditPage/{service.GetPageWithRoute(route).ID}");
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
                    Route = o.Route

                };

            service.AddPage(pageModel);

            return "Success"; 
        }



        [HttpPost]
        public string Update(JSONForSavingWebPage o)
        {
            PageModel pageModel = new PageModel()
            {
                Header = o.Header,
                Body = o.Body,
                Footer = o.Footer,
                CSS = o.CSS,
                Route = o.Route,
                ImageUrls = o.ImageURLS
                    
                };

            service.AddPage(pageModel);

            return "Success"; 
        }

        [HttpPost]
        public string UpdatePage(JSONForSavingWebPage o)
        {
            PageModel pageModel = new PageModel()
            {
                Header = o.Header,
                Body = o.Body,
                Footer = o.Footer,
                CSS = o.CSS,
                Route = o.Route

            };

            service.AddPage(pageModel);

            return "Success";
        }

    }
}
