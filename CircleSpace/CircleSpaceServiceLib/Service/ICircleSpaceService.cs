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
        PageModel GetPageRoute(string route);
        List<PageModel> GetContributorPages(UserModel model);
        void UpdatePage(PageModel model);
        void AddContributorToPage(PageModel page, UserModel contributor);
        void DeleteContributorFromPage(PageModel page, UserModel contributor);
        LayoutModel GetLayoutWithOwner(UserModel model);
        LayoutModel GetLayoutWithTag(params string[] tags);
        LayoutModel GetLayoutWithType(LayoutTypes type);
        void AddLayout(LayoutModel model);
        void UpdateLayout(LayoutModel model);

        List<LayoutModel> GetLayouts();

    }
}
