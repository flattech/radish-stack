
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Repositories.Enums;

namespace Core.Repositories
{
    public class MenuRepository : BaseRepository<MenuItem>
    {
        public MenuRepository()
        {
            base.Init("MenuItem", "Title,ParentId,EntityId,EntityName,Url,DisplayOrder,IsMega,IsActive,IncludeInHeader,IncludeInFooter,WidgetId");
        }
        public new List<MenuItem> GetAll()
        {
            return GetAllItems("", "DisplayOrder").ToList();
        }
        public IEnumerable<MenuItem> GetTree()
        {
            return GetAll().SortMenuForTree();
        }
    }

    public static class MenuExtentions
    {
        public static IList<MenuItem> SortMenuForTree(this IList<MenuItem> source, Guid? parentId = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<MenuItem>();

            foreach (var cat in source.Where(c => c.ParentId == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(SortMenuForTree(source,cat.Id));
            }
            //if (result.Count != source.Count)
            //{
            //    //find categories without parent in provided Term source and insert them into result
            //    foreach (var cat in source)
            //        if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
            //            result.Add(cat);
            //}
            return result;
        }


        public static string GetFormattedMenuBreadCrumb(this MenuItem Term, IList<MenuItem> allCategories, string separator = ">>", int languageId = 0)
        {
            string result = string.Empty;

            var breadcrumb = GetMenuBreadCrumb(Term, allCategories, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var termName = breadcrumb[i].Title;//.GetLocalized(x => x.Title, languageId);
                result = String.IsNullOrEmpty(result) ? termName : $"{result} {separator} {termName}";
            }

            return result;
        }

        public static IList<MenuItem> GetMenuBreadCrumb(this MenuItem menu, IList<MenuItem> allCategories, bool showHidden = false)
        {
            if (menu == null)
                throw new ArgumentNullException("Menu is null");

            var result = new List<MenuItem>();

            //used to prevent circular references
            var alreadyProcessedTermIds = new List<Guid>();

            while (menu != null &&
                (showHidden
                // || Term.IsActive
                ) && //published
                 !alreadyProcessedTermIds.Contains(menu.Id)) //prevent circular references
            {
                result.Add(menu);

                alreadyProcessedTermIds.Add(menu.Id);

                menu = (from c in allCategories
                        where c.Id == menu.ParentId
                        select c).FirstOrDefault();
            }
            result.Reverse();
            return result;
        }
    }

    public class MenuItem : BaseModel
    {
        public MenuItem()
        {
            Children =new List<MenuItem>();
        }
        public string Title { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? EntityId { get; set; }
        public string EntityName { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMega { get; set; }
        public bool IncludeInHeader { get; set; }
        public bool IncludeInFooter { get; set; }
        public List<MenuItem> Children { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public Guid? WidgetId { get; set; }
    }

}
