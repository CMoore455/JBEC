using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace CircleSpace.Models
{
    public class ProfileContentContainer
    {
        public readonly ReadOnlyCollection<string> PAGE_ROUTES_OF_OWNED_PAGES;
        public readonly ReadOnlyCollection<string> PAGE_ROUTES_OF_CONTRIUBTED_PAGES;
        public ReadOnlyCollection<int> OWNED_LAYOUT_IDS;

        public ProfileContentContainer(ReadOnlyCollection<string> pageRoutesOfOwnedPages, ReadOnlyCollection<string> pageRoutesOfContributedPages, ReadOnlyCollection<int> ownedLayoutIDs)
        { 
            this.PAGE_ROUTES_OF_OWNED_PAGES = pageRoutesOfOwnedPages ?? new List<string>().AsReadOnly();
            this.PAGE_ROUTES_OF_OWNED_PAGES = pageRoutesOfContributedPages ?? new List<string>().AsReadOnly();
            this.OWNED_LAYOUT_IDS = ownedLayoutIDs ?? new List<int>().AsReadOnly();
        }
    }
}