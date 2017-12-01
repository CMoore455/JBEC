using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircleSpaceGeneralModels.Enums;
using CircleSpaceGeneralModels.Models;
using CircleSpaceDAL;

namespace CircleSpaceServiceLib.Service
{
    public class SqlCricleSpaceService : ICircleSpaceService
    {
        public void AddContributorToPage(PageModel page, UserModel contributor)
        {
            using (var db = new CircleSpaceEntities())
            {
                var query = db.Pages.Where(p => p.ID == page.ID).First();
                query.AspNetUsers.Add(db.AspNetUsers.Where(u => u.Id == contributor.ID).First());

                db.SaveChanges();
            }
        }

        public void AddLayout(LayoutModel model)
        {
            using (var db = new CircleSpaceEntities())
            {
                var newLayout = new Layout()
                {
                    ID = db.Layouts.Max(l => l.ID) + 1,
                    OwnerID = model.Owner.ID,
                    LayoutTitle = model.LayoutTitle,
                    Content = model.Content,
                    CSS = model.CSS,
                    LayoutType = model.Type.ToString()
                };
                db.Layouts.Add(newLayout);
                model.Tags.ForEach(tag =>
                {
                    var newTag = new Tag()
                    {
                        Tag1 = tag,
                        ID = db.Tags.Max(t => t.ID) + 1,
                        LayoutID = newLayout.ID
                    };
                    db.Tags.Add(newTag);
                });
                db.SaveChanges();
            }
        }



        public void AddPage(PageModel model)
        {
            using (var db = new CircleSpaceEntities())
            {
                var newPage = new Page()
                {
                    PageRoute = model.Route,
                    ID = db.Pages.Max(p => p.ID) + 1,
                    OwnerID = model.OwnerID,
                    Header = model.Header,
                    Body = model.Body,
                    Footer = model.Footer
                };
            }
        }
        public void DeleteContributorFromPage(PageModel page, UserModel contributor)
        {
            using (var db = new CircleSpaceEntities())
            {

                var query = db.Pages.Where(p => p.ID == page.ID).First();
                query.AspNetUsers.Remove(db.AspNetUsers.Where(u => u.Id == contributor.ID).First());
                db.SaveChanges();

            }
        }

        public void DeletePage(PageModel model)
        {
            using (var db = new CircleSpaceEntities())
            {
                var pageToDelete = db.Pages.Where(p => p.ID == model.ID).First();
                db.Pages.Remove(pageToDelete);
                db.SaveChanges();
            }
        }

        public List<PageModel> GetContributorPages(UserModel model)
        {
            List<PageModel> list = new List<PageModel>();
            using (var db = new CircleSpaceEntities())
            {
                var user = db.AspNetUsers.Where(u => u.Id == model.ID).First();
                var pages = user.Pages1.ToList();
                pages.ForEach(page =>
                {
                    PageModel p = PageToPageModel(page);
                    list.Add(p);
                });
            }

            return list;
        }

        public List<LayoutModel> GetLayouts()
        {
            List<LayoutModel> list = new List<LayoutModel>();
            using (var db = new CircleSpaceEntities())
            {
                var query = db.Layouts.Select(x => x);
                list = LayoutsToListOfLayouts(query.ToList());
            }

            return list;
        }

        public LayoutModel GetLayoutWithID(int id)
        {
            LayoutModel l = new LayoutModel();
            using (var db = new CircleSpaceEntities())
            {
                l = LayoutToLayoutModel(db.Layouts.Where(x => x.ID == id).First());
            }
            return l;
        }

        public List<LayoutModel> GetLayoutWithOwner(UserModel model)
        {
            List<LayoutModel> l = new List<LayoutModel>();
            using (var db = new CircleSpaceEntities())
            {
                l = LayoutsToListOfLayouts(db.Layouts.Where(x => x.OwnerID == model.ID));
            }
            return l;
        }

        public List<LayoutModel> GetLayoutsWithTag(params string[] tags)
        {

            List<LayoutModel> layouts = null;

            using (var db = new CircleSpaceEntities())
            {
                List<Tag> tagsAsTagList = new List<Tag>();
                foreach (var tag in tags)
                {
                    tagsAsTagList.Add(new Tag() { Tag1 = tag });
                }
                tagsAsTagList = tagsAsTagList.OrderBy(tag => tag.Tag1).ToList();
                var query = from layout in db.Layouts
                            where layout.Tags.OrderBy(tag => tag.Tag1).SequenceEqual(tagsAsTagList, new TagNameComparer())
                            select layout;

                layouts = LayoutsToListOfLayouts(query.ToList());
            }
            return layouts;

        }

        public List<LayoutModel> GetLayoutsWithType(LayoutTypes type)
        {
            List<LayoutModel> l = new List<LayoutModel>();
            using (var db = new CircleSpaceEntities())
            {
                var query = db.Layouts.Where(x => x.LayoutType == type.ToString());
                l = LayoutsToListOfLayouts(query.ToList());
            }
            return l;
        }

        public PageModel GetPageWithRoute(string route)
        {
            PageModel p = new PageModel();
            using (var db = new CircleSpaceEntities())
            {
                p = PageToPageModel(db.Pages.Where(x => x.PageRoute == route).First());
            }
            return p;
        }

        public PageModel GetPageWithID(int id)
        {
            PageModel p = new PageModel();
            using (var db = new CircleSpaceEntities())
            {
                p = PageToPageModel(db.Pages.Where(x => x.ID == id).First());
            }
            return p;
        }

        public void UpdateLayout(LayoutModel model)
        {
            using (var db = new CircleSpaceEntities())
            {
                var layout = db.Layouts.Where(x => x.ID == model.ID).First();
                layout.LayoutTitle = model.LayoutTitle;
                layout.LayoutType = model.Type.ToString();
                layout.Content = model.Content;
                layout.CSS = model.CSS;
                var listOfTags = layout.Tags.ToList();
                listOfTags.ForEach(tag =>
                {
                    layout.Tags.Remove(tag);

                });
                model.Tags.ForEach(tag =>
                {
                    var newTag = new Tag()
                    {
                        Tag1 = tag,
                        ID = db.Tags.Max(t => t.ID) + 1,
                        LayoutID = model.ID
                    };
                    db.Tags.Add(newTag);
                });
                db.SaveChanges();
            }
        }

        public void UpdatePage(PageModel model)
        {
            using (var db = new CircleSpaceEntities())
            {
                var page = db.Pages.Where(x => x.ID == model.ID).First();
                page.PageRoute = model.Route;
                page.OwnerID = model.OwnerID;
                page.Header = model.Header;
                page.Body = model.Body;
                page.Footer = model.Footer;
                page.CSS = model.CSS;
                db.SaveChanges();
            }
        }

        private List<PageModel> PagesToPageModelList(List<Page> p)
        {
            List<PageModel> list = new List<PageModel>();
            p.ForEach(page =>
            {
                var newPage = PageToPageModel(page);
                list.Add(newPage);
            });
            return list;

        }


        /// <summary>
        /// This method will convert Page objects into PageModel objects
        /// </summary>
        /// <param name="p">The page being converted</param>
        /// <returns>The converted Page object as a PageModel object</returns>
        private PageModel PageToPageModel(Page p)
        {

            PageModel newPageModel = new PageModel()
            {
                Route = p.PageRoute,
                ID = p.ID,
                OwnerID = p.OwnerID,
                Header = p.Header,
                Body = p.Body,
                Footer = p.Footer,
                ImageUrls = ImagesToImageUrls(p),
                CSS = p.CSS,
                Contributors = AspNetUsersToContributors(p)

            };
            return newPageModel;
        }

        /// <summary>
        /// This method converts Image objects in to a list of strings called ImageUrls
        /// </summary>
        /// <param name="p">P object that holds the collection of Image objects</param>
        /// <returns>list of type string</returns>
        private List<string> ImagesToImageUrls(Page p)
        {
            List<string> list = new List<string>();
            var images = p.Images.ToList();
            images.ForEach(image =>
            {
                list.Add(image.ImageName);
            });
            return list;
        }
        //private ICollection<Image> ImageUrlsToImages(PageModel p)
        //{
        //    ICollection<Image> list = new List<Image>();
        //    var images = p.Images.ToList();
        //    images.ForEach(image =>
        //    {
        //        list.Add(image.ImageName);
        //    });
        //    return list;
        //}

        private UserModel UserToUserModels(AspNetUser u)
        {
            UserModel newUser = new UserModel()
            {
                ID = u.Id,
                Username = u.UserName,
                Email = u.Email

            };
            return newUser;
        }


        public List<PageModel> GetPagesWithOwnerID(string v)
        {
            List<PageModel> list = new List<PageModel>();
            using (var db = new CircleSpaceEntities())
            {
                 list = PagesToPageModelList(db.Pages.Where(p=> p.OwnerID == v).ToList());
            }
            return list;
        }

        public List<PageModel> GetContributorPagesWithOwnerID(string v)
        {
            List<PageModel> list = new List<PageModel>();
            using (var db = new CircleSpaceEntities())
            {

                list = PagesToPageModelList(db.Pages.Where(p => p.AspNetUsers.Select(u=> u.Id).Contains(v)).ToList());
            }
            return list;
        }

        public List<LayoutModel> GetLayoutWithOwnerID(string v)
        {
            List<LayoutModel> list = new List<LayoutModel>();
            using (var db = new CircleSpaceEntities())
            {
                list = LayoutsToListOfLayouts(db.Layouts.Where(l => l.OwnerID == v).ToList());
            }
            return list;
        }

        private List<UserModel> AspNetUsersToContributors(Page p)
        {
            List<UserModel> contributors = new List<UserModel>();
            var AspNetUsers = p.AspNetUsers.ToList();
            AspNetUsers.ForEach(user =>
            {
                contributors.Add(UserToUserModels(user));
            });
            return contributors;
        }

        private List<LayoutModel> LayoutsToListOfLayouts(IEnumerable<Layout> list)
        {
            List<LayoutModel> result = new List<LayoutModel>();
            list.ToList().ForEach(layout =>
            {
                var l = LayoutToLayoutModel(layout);
                result.Add(l);
            });
            return result;

        }
        private LayoutModel LayoutToLayoutModel(Layout l)
        {
            LayoutModel newLayout = new LayoutModel();
            using (var db = new CircleSpaceEntities())
            {
                newLayout = new LayoutModel()
                {
                    ID = l.ID,
                    Owner = UserToUserModels(db.AspNetUsers.Where(u => u.Id == l.OwnerID).First()),
                    LayoutTitle = l.LayoutTitle,
                    Content = l.Content,
                    CSS = l.CSS,
                    Type = (LayoutTypes)Enum.Parse(typeof(LayoutTypes), l.LayoutType)
                };
                var tags = l.Tags.ToList();
                tags.ForEach(tag =>
                {
                    newLayout.Tags.Add(tag.Tag1);
                });
            }

            return newLayout;

        }

        private Layout LayoutModelToLayout(LayoutModel l)
        {
            Layout newLayout = new Layout()
            {
                ID = l.ID,
                LayoutTitle = l.LayoutTitle,
                LayoutType = l.Type.ToString(),
                Content = l.Content,
                CSS = l.CSS,

            };
            return newLayout;
        }

       

        private class TagNameComparer : IEqualityComparer<Tag>
        {
            public bool Equals(Tag x, Tag y)
            {
                return x.Tag1 == y.Tag1;
            }

            public int GetHashCode(Tag obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}