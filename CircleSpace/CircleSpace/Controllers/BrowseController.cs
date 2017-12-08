using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircleSpaceServiceLib;
using CircleSpaceServiceLib.Service;
using CircleSpaceGeneralModels.Models;

namespace CircleSpace.Controllers
{
    [AllowAnonymous]
    public class BrowseController : Controller
    {
        ICircleSpaceService service = new SqlCricleSpaceService();
        // GET: Browse
        public ActionResult Index()
        {
            PageModel p = new PageModel();
            
            return View(p);
        }
        public ActionResult BrowseSite()
        {
            PageLayoutListModel model = new PageLayoutListModel(service.GetPages(), service.GetLayouts());     
            return View(model);
        }

        public string UserContent(string[] routeValues)
        {

            var route = string.Join("/", routeValues ?? new[] { "" });
            var page = service.GetPageWithRoute(route);

            string content = "<style>" + page.CSS + "</style>";
            content += page.Header + page.Body + page.Footer;
            //  return routeValues == null ? "NULL" : route;
            return content;
        }
        
    }
}