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
        ICircleSpaceService service;
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
            List<LayoutModel> ownedLayouts = service.GetLayoutWithOwnerID(User.Identity.GetUserId());

            profileContent = new ProfileContentContainer(ownedPages.AsReadOnly(), contributedPages.AsReadOnly(), ownedLayouts.AsReadOnly());

            return View(profileContent);
        }
    }
}