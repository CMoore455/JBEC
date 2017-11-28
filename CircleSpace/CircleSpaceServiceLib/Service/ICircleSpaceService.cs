using CircleSpaceGeneralModels.Enums;
using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleSpaceServiceLib.Service
{
    public interface ICircleSpaceService
    {
        void AddPage(PageModel model);
        void DeletePage(PageModel model);
        PageModel GetPageWithID(int id);
        PageModel GetPageWithRoute(string route);
        List<PageModel> GetContributorPages(UserModel model);
        void UpdatePage(PageModel model);
        void AddContributorToPage(PageModel page, UserModel contributor);
        void DeleteContributorFromPage(PageModel page, UserModel contributor);
        List<LayoutModel> GetLayoutWithOwner(UserModel model);
        LayoutModel GetLayoutWithID(int id);
        List<LayoutModel> GetLayoutsWithTag(params string[] tags);
        List<LayoutModel> GetLayoutsWithType(LayoutTypes type);
        List<PageModel> GetPagesWithOwnerID(string v);
        void AddLayout(LayoutModel model);
        List<PageModel> GetContributorPagesWithOwnerID(string v);
        void UpdateLayout(LayoutModel model);
        List<LayoutModel> GetLayoutWithOwnerID(string v);
        List<LayoutModel> GetLayouts();
        
    }
}
