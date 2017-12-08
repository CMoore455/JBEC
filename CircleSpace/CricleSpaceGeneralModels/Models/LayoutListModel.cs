using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleSpaceGeneralModels.Models
{
    public class LayoutListModel
    {
        public List<LayoutModel> Layouts { get; set; }

        public LayoutListModel(List<LayoutModel> list)
        {
            Layouts = list;
        }
    }
}
