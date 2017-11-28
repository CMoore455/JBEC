using CircleSpace.Models;
using CircleSpaceServiceLib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CircleSpaceGeneralModels.Models;

namespace CircleSpace.Controllers
{
    public class HomeController : Controller
    {
        ICircleSpaceService service = new SqlCricleSpaceService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult ProfilePage()
        {
            ProfileContentContainer profileContent;
            List<PageModel> ownedPages = service.GetPagesWithOwnerID(User.Identity.GetUserId());
            List<PageModel> contributedPages = service.GetContributorPagesWithOwnerID(User.Identity.GetUserId());
            List<LayoutModel> ownedLayout = service.GetLayoutWithOwnerID(User.Identity.GetUserId());

            List<string> ownedPagesRoutes = (from page in ownedPages
                                            select page.Route).ToList();
            List<string> contriubtedPagesRoutes = (from page in contributedPages
                                                   select page.Route).ToList();
            List<int> ownedLayoutIDs = (from layout in ownedLayout
                                        select layout.ID).ToList();

            profileContent = new ProfileContentContainer(ownedPagesRoutes.AsReadOnly(), contriubtedPagesRoutes.AsReadOnly(), ownedLayoutIDs.AsReadOnly());

            return View(profileContent);
        }
    }
}