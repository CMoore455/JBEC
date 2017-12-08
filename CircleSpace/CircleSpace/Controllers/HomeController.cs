using CircleSpace.Models;
using CircleSpaceServiceLib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CircleSpaceGeneralModels.Models;
using System.IO;
using System.Text;
using System.IO.Compression;

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
            ViewBag.Message = "CircleSpace is an easy to use, website creator with built in modifiable templates, that make building a website possible for even those who have no prior coding knowledge.​";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CircleSpace Headquarters";

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

            return View("ProfilePage", profileContent);
        }


        [Authorize]
        public ActionResult DeleteOwnedPage(int id)
        {
            var page = service.GetPageWithID(id);
            if(page.OwnerID == User.Identity.GetUserId())
            {
                service.DeletePage(page);
            }
            return ProfilePage();
        }

        [Authorize]
        public ActionResult SaveCreatedPage(int id)
        {
            var page = service.GetPageWithID(id);
            if (string.IsNullOrWhiteSpace(page.Route)) { page.Route = "WebPage"; }
            string html = page.Header + page.Body + page.Footer;
            string css = page.CSS;

            byte[] htmlFileContents = Encoding.Default.GetBytes(html.Replace("</head>", $"<link rel=\"stylesheet\" type=\"text / css\" href=\"{page.Route.Replace('/', '_')}.css\"> </head>"));
            byte[] cssFileContents = Encoding.Default.GetBytes(css);
            byte[] zipFileContents = null;

            using (var zipStream = new MemoryStream())
            {

                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    ZipArchiveEntry htmlArchiveEntry = zip.CreateEntry($"{page.Route.Replace('/', '_')}.html", CompressionLevel.Fastest);

                    using (var zipFileStream = htmlArchiveEntry.Open())
                    {
                        zipFileStream.Flush();
                        zipFileStream.Write(htmlFileContents, 0, htmlFileContents.Length);
                    }

                    ZipArchiveEntry cssArchiveEntry = zip.CreateEntry($"{page.Route.Replace('/', '_')}.css", CompressionLevel.Fastest);

                    using (var zipFileStream = cssArchiveEntry.Open())
                    {
                        zipFileStream.Flush();
                        zipFileStream.Write(cssFileContents, 0, cssFileContents.Length);
                    }
                }
                zipStream.Seek(0, SeekOrigin.Begin);
                zipFileContents = new byte[zipStream.Length];
                zipStream.Read(zipFileContents, 0, zipFileContents.Length);
            }

            return File(zipFileContents, "application/zip", $"{page.Route.Replace('/', '_')}.zip");
        }

        [Authorize]
        public ActionResult SaveCreatedLayout(int id)
        {
            var layout = service.GetLayoutWithID(id);
            if (string.IsNullOrWhiteSpace(layout.LayoutTitle)) { layout.LayoutTitle = "Layout"; }
            string html = layout.Content;
            string css = layout.CSS;

            byte[] htmlFileContents = Encoding.Default.GetBytes(html);
            byte[] cssFileContents = Encoding.Default.GetBytes(css);
            byte[] zipFileContents = null;

            using (var zipStream = new MemoryStream())
            {

                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    ZipArchiveEntry htmlArchiveEntry = zip.CreateEntry($"{layout.LayoutTitle.Replace('/', '_')}.html", CompressionLevel.Fastest);

                    using (var zipFileStream = htmlArchiveEntry.Open())
                    {
                        zipFileStream.Flush();
                        zipFileStream.Write(htmlFileContents, 0, htmlFileContents.Length);
                    }

                    ZipArchiveEntry cssArchiveEntry = zip.CreateEntry($"{layout.LayoutTitle.Replace('/', '_')}.css", CompressionLevel.Fastest);

                    using (var zipFileStream = cssArchiveEntry.Open())
                    {
                        zipFileStream.Flush();
                        zipFileStream.Write(cssFileContents, 0, cssFileContents.Length);
                    }
                }

                zipStream.Seek(0, SeekOrigin.Begin);
                zipFileContents = new byte[zipStream.Length];
                zipStream.Read(zipFileContents, 0, zipFileContents.Length);
            }

            return File(zipFileContents, "application/zip", $"{layout.LayoutTitle.Replace('/', '_')}.zip");
        }
    }
}