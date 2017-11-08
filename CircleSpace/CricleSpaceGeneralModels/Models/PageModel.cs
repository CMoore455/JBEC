using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricleSpaceGeneralModels.Models
{
    public class PageModel
    {
        private string header;

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        private string footer;

        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }

        private string body;

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        private string route;

        public string Route
        {
            get { return route; }
            set { route = value; }
        }

        private string css;

        public string CSS
        {
            get { return css; }
            set { css = value; }
        }

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private List<string> imageUrls;

        public List<string> ImageUrls
        {
            get { return imageUrls; }
            set { imageUrls = value; }
        }

    }
}
