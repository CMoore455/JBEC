using System.Collections.Generic;
using CircleSpaceGeneralModels.Enums;
using System.Web.Mvc;

namespace CircleSpace.Models
{
    public class JSONForSavingLayout
    {
        string title, content, css;
        int id;
        LayoutTypes type;

        public string Title { get; set; }
        [AllowHtml]
        public string Content { get;  set; }
        public int ID { get;  set; }
        public string CSS { get; set; }
        public List<string> Tags { get; set; }
        public LayoutTypes Type { get;  set; }
    }
}