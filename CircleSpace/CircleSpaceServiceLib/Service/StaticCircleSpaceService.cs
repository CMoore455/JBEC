using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircleSpaceGeneralModels.Enums;
using CircleSpaceGeneralModels.Models;

namespace CircleSpaceServiceLib.Service
{
    public class StaticCircleSpaceService : ICircleSpaceService
    {
        public void AddContributorToPage(PageModel page, UserModel contributor)
        {
            throw new NotImplementedException();
        }

        public void AddLayout(LayoutModel model)
        {
            throw new NotImplementedException();
        }

        public void AddPage(PageModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteContributorFromPage(PageModel page, UserModel contributor)
        {
            throw new NotImplementedException();
        }

        public void DeletePage(PageModel model)
        {
            throw new NotImplementedException();
        }

        public List<PageModel> GetContributorPages(UserModel model)
        {
            throw new NotImplementedException();
        }

        public List<PageModel> GetContributorPagesWithOwnerID(string v)
        {
            throw new NotImplementedException();
        }

        public List<LayoutModel> GetLayouts()
        {
            throw new NotImplementedException();
        }

        public List<LayoutModel> GetLayoutsWithTag(params string[] tags)
        {
            throw new NotImplementedException();
        }

        public List<LayoutModel> GetLayoutsWithType(LayoutTypes type)
        {
            throw new NotImplementedException();
        }

        public LayoutModel GetLayoutWithID(int id)
        {
            throw new NotImplementedException();
        }

        public List<LayoutModel> GetLayoutWithOwner(UserModel model)
        {
            throw new NotImplementedException();
        }

        public List<LayoutModel> GetLayoutWithOwnerID(string v)
        {
            throw new NotImplementedException();
        }

        public List<PageModel> GetPagesWithOwnerID(string v)
        {
            throw new NotImplementedException();
        }

        public PageModel GetPageWithID(int id)
        {
            throw new NotImplementedException();
        }

        public PageModel GetPageWithRoute(string route)
        {
            throw new NotImplementedException();
        }

        public void UpdateLayout(LayoutModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdatePage(PageModel model)
        {
            throw new NotImplementedException();
        }
    }
}
