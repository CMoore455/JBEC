using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleSpaceGeneralModels.Models
{
    public class UserModel
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        //private List<PageModel> ownedPages;

        //public List<PageModel> OwnedPages
        //{
        //    get { return ownedPages; }
        //    set { ownedPages = value; }
        //}

        //private List<LayoutModel> ownedLayouts;

        //public List<LayoutModel> OwnedLayouts
        //{
        //    get { return ownedLayouts; }
        //    set { ownedLayouts = value; }
        //}

    }
}
