using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircleSpace.Models
{
    public class JSONForSavingWebPage
    {
        private string header = "", body = "", footer = "", css = "";

        [AllowHtml]
        public string Header { get { return header; } set { header = value ?? ""; } }
        [AllowHtml]
        public string Body { get { return body; } set { body = value ?? ""; } }
        [AllowHtml]
        public string Footer { get => footer; set => footer = value ?? ""; }

        public string CSS { get => css; set => css = value ?? ""; }

        public int ID { get; set; }
        //Need Route
        //Need ImageURLS
    }
}