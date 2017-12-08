using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricleSpaceGeneralModels.Models
{
    public class PageLayoutListModel
    {
        public List<PageModel> Pages { get; set; }
        public List<LayoutModel> Layouts { get; set; }

        public PageLayoutListModel(List<PageModel> page, List<LayoutModel> layout)
        {
            Pages = page;
            Layouts = layout;
        }
    }
}
