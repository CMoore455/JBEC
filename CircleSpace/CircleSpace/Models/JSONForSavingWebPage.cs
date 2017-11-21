using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircleSpace.Models
{
    public class JSONForSavingWebPage
    {
        [AllowHtml]
        public string Header { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        [AllowHtml]
        public string Footer { get; set; }
        public string CSS { get; set; }

        //Need Route
        //Need ImageURLS

    }
}