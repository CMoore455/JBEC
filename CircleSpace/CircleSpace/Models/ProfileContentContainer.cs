using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace CircleSpace.Models
{
    public class ProfileContentContainer
    {
        
        public readonly ReadOnlyCollection<PageModel> OWNED_PAGES;
        public readonly ReadOnlyCollection<PageModel> CONTRIUBTED_PAGES;
        public ReadOnlyCollection<LayoutModel> OWNED_LAYOUTS;

        public ProfileContentContainer(ReadOnlyCollection<PageModel> pageRoutesOfOwnedPages, ReadOnlyCollection<PageModel> pageRoutesOfContributedPages, ReadOnlyCollection<LayoutModel> ownedLayoutIDs)
        { 
            this.OWNED_PAGES = pageRoutesOfOwnedPages ?? new List<PageModel>().AsReadOnly();
            this.OWNED_PAGES = pageRoutesOfContributedPages ?? new List<PageModel>().AsReadOnly();
            this.OWNED_LAYOUTS = ownedLayoutIDs ?? new List<LayoutModel>().AsReadOnly();
        }
    }
}