using System.Collections.Generic;
using CircleSpaceGeneralModels.Enums;
using CircleSpaceGeneralModels.Models;
using CircleSpaceServiceLib.Service;

namespace CircleSpace.Controllers
{
    public class DummyCircleSpaceService : ICircleSpaceService
    {
        public void AddContributorToPage(PageModel page, UserModel contributor)
        {
            throw new System.NotImplementedException();
        }

        public void AddLayout(LayoutModel model)
        {
            throw new System.NotImplementedException();
        }

        public void AddPage(PageModel model)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteContributorFromPage(PageModel page, UserModel contributor)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePage(PageModel model)
        {
            throw new System.NotImplementedException();
        }

        public List<PageModel> GetContributorPages(UserModel model)
        {
            throw new System.NotImplementedException();
        }

        public LayoutModel GetLayoutWithOwner(UserModel model)
        {
            throw new System.NotImplementedException();
        }

        public LayoutModel GetLayoutWithTag(params string[] tags)
        {
            throw new System.NotImplementedException();
        }

        public LayoutModel GetLayoutWithType(LayoutTypes type)
        {
            throw new System.NotImplementedException();
        }

        public PageModel GetPageRoute(string route)
        {
            throw new System.NotImplementedException();
        }

        public PageModel GetPageWithID(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<LayoutModel> GetLayouts()
        {
            return new List<LayoutModel>() {

                new LayoutModel(){  LayoutTitle = "Header 1", ID = 1, Content = "<h1>Header 1</h1>", CSS = "body{font-weight: bold;}", Tags = new List<string>(){ "Happy" }, Type = LayoutTypes.Header },
                new LayoutModel(){ ID = 2, Content = "<h1>Header 2</h1>", CSS = "body{font-weight: bold;}", Tags = new List<string>(){ "Happy" }, Type = LayoutTypes.Header },
                new LayoutModel(){ ID = 3, Content = "<h1>Header 3</h1>", CSS = "body{font-weight: bold;}", Tags = new List<string>(){ "Happy" }, Type = LayoutTypes.Header },
                new LayoutModel(){ ID = 4, Content = "<p>Body 1</p>", CSS = "body{font-weight: bold;}", Tags = new List<string>(){ "Happy" }, Type = LayoutTypes.Body },
                new LayoutModel(){ ID = 5, Content = "<p>Body 2</p>", CSS = "body{font-weight: bold;}", Tags = new List<string>(){ "Happy" }, Type = LayoutTypes.Body },
                new LayoutModel(){ ID = 6, Content = "<p>Body 3</p>", CSS = "body{font-weight: bold;}", Tags = new List<string>(){ "Happy" }, Type = LayoutTypes.Body },
                new LayoutModel(){ ID = 7, Content = "<footer>Footer</footer>", CSS = "body{font-weight: bold", Tags = new List<string>(){ "Happy" }, Type = LayoutTypes.Footer }
            };
        }

        public void UpdateLayout(LayoutModel model)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePage(PageModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}