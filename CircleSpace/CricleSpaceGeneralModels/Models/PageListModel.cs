using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircleSpaceGeneralModels.Models;
namespace CricleSpaceGeneralModels.Models
{
    public class PageListModel
    {
        public List<PageModel> Pages { get; set; }

        public PageListModel(List<PageModel> list)
        {
            Pages = list;
        }
    }
}
