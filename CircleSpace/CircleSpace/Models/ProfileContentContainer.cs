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

        public ProfileContentContainer(ReadOnlyCollection<PageModel> ownedPages, ReadOnlyCollection<PageModel> contributedPages, ReadOnlyCollection<LayoutModel> ownedLayouts)
        { 
            this.OWNED_PAGES = ownedPages ?? new List<PageModel>().AsReadOnly();
            this.CONTRIUBTED_PAGES = contributedPages ?? new List<PageModel>().AsReadOnly();
            this.OWNED_LAYOUTS = ownedLayouts ?? new List<LayoutModel>().AsReadOnly();
        }
    }
}