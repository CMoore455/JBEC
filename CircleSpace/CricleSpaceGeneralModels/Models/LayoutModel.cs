using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricleSpaceGeneralModels.Models
{
    public class LayoutModel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string css;

        public string CSS
        {
            get { return css; }
            set { css = value; }
        }

        private UserModel owner;

        public UserModel Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private List<string> tags;

        public List<string> Tags
        {
            get { return tags; }
            set { tags = value; }
        }



    }

}
