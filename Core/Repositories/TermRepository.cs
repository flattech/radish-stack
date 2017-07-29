using System;
using System.Collections.Generic;
using System.Linq;
using Core.Repositories.Enums;

namespace Core.Repositories
{
    public class TermRepository : BaseRepository<Term>
    {
        public TermRepository()
        {
            base.Init("Term", "Title,ParentId,TaxonomyId,IncludeInTopMenu,PostTypeId,DisplayOrder,IsPublic,IsActive,UrlKey");
        }
        
        public IList<Term> GetTree(Guid postTypeId)
        {
            return
                GetAllItems("TaxonomyId=" + (int)(TermTypeEnum.Tree) + " and PostTypeId='" + postTypeId + "'").ToList().SortTermsForTree();
        }

        public IList<Term> GetAll(TermTypeEnum t)
        {
            return
                GetAll("TaxonomyId", ((int)t).ToString()).ToList();
        }

        public IList<Term> GetByPostType(Guid posttypeid)
        {
            return
                GetAll("PostTypeId", (posttypeid).ToString()).ToList();
        }
        public List<Term> GetAll(string[] tags,Guid posttypeid)
        {
            string intitle="N'" + string.Join("',N'", tags) + "'";
            return Query<Term>(SqlSelect + string.Format(" WHERE Status!=-100 and TaxonomyId=20 and " +
                                                         "PostTypeId='{1}' and  [Title] in ({0})", intitle, posttypeid)).ToList();
        }
     
    }

    public static class  TermExtentions
    {
        public static bool IsTag(this Term Term)
        {
            return Term.TaxonomyId == (int)TermTypeEnum.Tag;
        }

        public static IList<Term> SortTermsForTree(this IList<Term> source, Guid? parentId = null, bool ignoreWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<Term>();

            foreach (var cat in source.Where(c => c.ParentId == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(SortTermsForTree(source, (Guid?)cat.Id, true));
            }
            if (!ignoreWithoutExistingParent && result.Count != source.Count)
            {
                //find categories without parent in provided Term source and insert them into result
                foreach (var cat in source)
                    if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                        result.Add(cat);
            }
            return result;
        }

        public static string GetFormattedBreadCrumb(this Term Term,
            TermRepository TermService,
            string separator = ">>", int languageId = 0)
        {
            string result = string.Empty;

            var breadcrumb = GetTermBreadCrumb(Term, TermService, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var TermName = breadcrumb[i].Title;//.GetLocalized(x => x.Title, languageId);
                result = String.IsNullOrEmpty(result)
                    ? TermName
                    : string.Format("{0} {1} {2}", result, separator, TermName);
            }

            return result;
        }

        public static string GetFormattedBreadCrumb(this Term Term,
            IList<Term> allCategories,
            string separator = ">>", int languageId = 0)
        {
            string result = string.Empty;

            var breadcrumb = GetTermBreadCrumb(Term, allCategories, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var TermName = breadcrumb[i].Title;//.GetLocalized(x => x.Title, languageId);
                result = String.IsNullOrEmpty(result)
                    ? TermName
                    : string.Format("{0} {1} {2}", result, separator, TermName);
            }

            return result;
        }

        public static IList<Term> GetTermBreadCrumb(this Term term,
            TermRepository TermService,
            bool showHidden = false)
        {
            if (term == null)
                throw new ArgumentNullException("Term");

            var result = new List<Term>();

            //used to prevent circular references
            var alreadyProcessedTermIds = new List<Guid>();

            while (term != null && //not null
                //!Term.IsDeleted && //not deleted
                (showHidden //|| Term.IsActive
                ) && //published
                !alreadyProcessedTermIds.Contains(term.Id)) //prevent circular references
            {
                result.Add(term);

                alreadyProcessedTermIds.Add(term.Id);

                term = term.ParentId.HasValue ? TermService.Get(term.ParentId.Value) : null;
            }
            result.Reverse();
            return result;
        }

        public static IList<Term> GetTermBreadCrumb(this Term Term,
            IList<Term> allCategories,
             bool showHidden = false)
        {
            if (Term == null)
                throw new ArgumentNullException("Term");

            var result = new List<Term>();

            //used to prevent circular references
            var alreadyProcessedTermIds = new List<Guid>();

            while (Term != null && //not null
               // !Term.IsDeleted && //not deleted
                (showHidden 
                //||
                //Term.IsActive
                ) && //published
                 !alreadyProcessedTermIds.Contains(Term.Id)) //prevent circular references
            {
                result.Add(Term);

                alreadyProcessedTermIds.Add(Term.Id);

                Term = (from c in allCategories
                        where c.Id == Term.ParentId
                        select c).FirstOrDefault();
            }
            result.Reverse();
            return result;
        }


        public static IList<MenuItem> SortTermsForTree(this IList<MenuItem> source, Guid? parentId = null, bool ignoreWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<MenuItem>();

            foreach (var cat in source.Where(c => c.ParentId == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(SortTermsForTree(source, cat.Id, true));
            }
            if (!ignoreWithoutExistingParent && result.Count != source.Count)
            {
                //find categories without parent in provided Term source and insert them into result
                foreach (var cat in source)
                    if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                        result.Add(cat);
            }
            return result;
        }


        public static string GetFormattedBreadCrumb(this MenuItem Term,
            IList<MenuItem> allCategories,
            string separator = ">>", int languageId = 0)
        {
            string result = string.Empty;

            var breadcrumb = GetTermBreadCrumb(Term, allCategories, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var TermName = breadcrumb[i].Title;//.GetLocalized(x => x.Title, languageId);
                result = String.IsNullOrEmpty(result)
                    ? TermName
                    : string.Format("{0} {1} {2}", result, separator, TermName);
            }

            return result;
        }

        public static IList<MenuItem> GetTermBreadCrumb(this MenuItem menu,
            IList<MenuItem> allCategories,
             bool showHidden = false)
        {
            if (menu == null)
                throw new ArgumentNullException("Term");

            var result = new List<MenuItem>();

            //used to prevent circular references
            var alreadyProcessedTermIds = new List<Guid>();

            while (menu != null && //not null
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
    public class Term : BaseModel
    {
        public string Title { get; set; }
        public Guid? ParentId { get; set; }
        public int TaxonomyId { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public Guid PostTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public virtual PostType PostType { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public string UrlKey { get; set; }
    }

}
