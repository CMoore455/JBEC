using CircleSpace.Models;
using CircleSpaceGeneralModels.Models;
using CircleSpaceServiceLib.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
         public ActionResult SavePage(JSONForSavingWebPage o)
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

            return new HttpStatusCodeResult(HttpStatusCode.NoContent); 
        }

        [HttpPost]
        public ActionResult UpdatePage(JSONForSavingWebPage o)
        {
            PageModel pageModel = new PageModel()
            {
                Header = o.Header,
                Body = o.Body,
                Footer = o.Footer,
                CSS = o.CSS,
                OwnerID = User.Identity.GetUserId(),
                ID = o.ID,
                //Need Route
                //Need ImageURLS
                Route = o.Route,
                ImageUrls = o.ImageURLS
            };

            service.UpdatePage(pageModel);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent); 
        }

        [HttpPost]
        public ActionResult SaveLayout(JSONForSavingLayout o)
        {
            LayoutModel layout = new LayoutModel()
            {
                LayoutTitle = o.Title,
                Content = o.Content,
                ID = o.ID,
                CSS = o.CSS,
                Tags = o.Tags,
                Type = o.Type
            };

            service.UpdateLayout(layout);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        public ActionResult EditPage(int id)
        {
            var page = service.GetPageWithID(id);
            var layouts = service.GetLayouts();
            var headers = (from header in layouts
                          where header.Type == CircleSpaceGeneralModels.Enums.LayoutTypes.Header
                          select header).ToList().AsReadOnly();
            var bodies = (from body in layouts
                         where body.Type == CircleSpaceGeneralModels.Enums.LayoutTypes.Body
                         select body).ToList().AsReadOnly();
            var footers = (from footer in layouts
                          where footer.Type == CircleSpaceGeneralModels.Enums.LayoutTypes.Footer
                          select footer).ToList().AsReadOnly();

            return View(new EditPageContentContainer(headers, bodies, footers, page));

            
        }

        public ActionResult EditLayout(int id)
        {
            return View(service.GetLayoutWithID(id));
        }

    }
}
