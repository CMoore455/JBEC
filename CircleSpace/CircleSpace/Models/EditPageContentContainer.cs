using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace CircleSpace.Models
{
    public class EditPageContentContainer
    {
        public readonly ReadOnlyCollection<LayoutModel> HEADERS;
        public readonly ReadOnlyCollection<LayoutModel> BODIES;
        public readonly ReadOnlyCollection<LayoutModel> FOOTERS;

        public readonly PageModel PAGE_TO_EDIT;

        public EditPageContentContainer(ReadOnlyCollection<LayoutModel> hEADERS, ReadOnlyCollection<LayoutModel> bODIES, ReadOnlyCollection<LayoutModel> fOOTERS, PageModel pageToEdit)
        {
            HEADERS = hEADERS ?? new List<LayoutModel>().AsReadOnly();
            BODIES = bODIES?? new List<LayoutModel>().AsReadOnly();
            FOOTERS = fOOTERS ?? new List<LayoutModel>().AsReadOnly();
            if(pageToEdit == null)
            {
                throw new ArgumentException("The page to edit cannot be null.");
            }
            this.PAGE_TO_EDIT = pageToEdit;
        }
    }
}