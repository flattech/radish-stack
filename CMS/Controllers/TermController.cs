using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS.Models;
using Core;
using Core.Extentions;
using Core.Repositories;
using Core.Repositories.Enums;

namespace CMS.Controllers
{
    [Authorize]
    public class TermController : BaseController
    {
        private readonly TermRepository _repository = Pool.Instance.Terms;

        public TermController()
        {
            ViewBag.Current = "Term";
        }

        public ActionResult Index(Guid posttypeid, int page = 1, int size = 10, string query = "", int taxonomyid = 0)
        {
            var q = Query("PostTypeId", posttypeid.ToString()) +
                    Query("and TaxonomyId", taxonomyid.ToString()) + QueryLike("and Title", query);

            var tax = (TermTypeEnum)taxonomyid;
            List<Term> terms;

            if (tax == TermTypeEnum.Tree)
            {
                var list = _repository.GetTree(posttypeid);
                terms = list.Select(x => new Term { Id = x.Id, Title = x.GetFormattedBreadCrumb(list, "--") }).ToList();
                if (!string.IsNullOrEmpty(query))
                    terms = terms.Where(x => x.Title.Contains(query)).ToList();
            }
            else
            {
                terms = _repository.GetAll(size, page, q).ToList();
            }

            var model = new TermModel
            {
                PostType = UOW.PostTypes.Get(posttypeid),
                total = _repository.GetCount(q),
                page = page,
                query = query,
                taxonomyid = taxonomyid,
                size = size,
                List = terms
            };
            return View(model);
        }

        public ActionResult Create(Guid posttypeid, int taxonomyId)
        {
            var model = new TermForm
            {
                PostType = UOW.PostTypes.Get(posttypeid),
                PostTypeId = posttypeid,
                IsActive = true,
                TaxonomyId = taxonomyId,
                Taxonomy = (TermTypeEnum)taxonomyId
            };
            PrepareAllTermsModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TermForm form)
        {
            if (string.IsNullOrEmpty(form.Title) || string.IsNullOrEmpty(form.UrlKey))
                ModelState.AddModelError("", "Title and UrlKey are Required");

            if (ModelState.IsValid)
            {
                var model = Map(form);
                UOW.Terms.Save(model);
                UOW.Commit();
                AlertSuccessMessage("Term is Successfully Saved");
                // LogActivity("CreateTerm", string.Format("{0} | {1}| {2} ", model.PostType.Name, model.Title, model.Id));
                return RedirectToAction("Edit", new { id = model.Id });
            }
            form.PostType = UOW.PostTypes.Get(form.PostTypeId);
            PrepareAllTermsModel(form);
            return View(form);
        }

        [NonAction]
        public virtual void PrepareAllTermsModel(TermForm model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableTerms.Add(new SelectListItem
            {
                Text = "[None]",
                Value = Guid.Empty.ToString()
            });
            var categories = UOW.Terms.GetTree(model.PostTypeId);
            foreach (var c in categories)
            {
                model.AvailableTerms.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(categories),
                    Value = c.Id.ToString()
                });
            }
        }

        public ActionResult Edit(Guid id)
        {
            var postcategory = UOW.Terms.Get(id);
            if (postcategory == null)
            {
                return HttpNotFound();
            }
            var model = Map(postcategory);
            PrepareAllTermsModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TermForm termForm)
        {
            if (string.IsNullOrEmpty(termForm.Title) || string.IsNullOrEmpty(termForm.UrlKey))
                ModelState.AddModelError("", "Title and UrlKey are Required");
            if (ModelState.IsValid)
            {
                var t = Map(termForm);
                UOW.Terms.Save(t);
                UOW.Commit();
                AlertSuccessMessage("Term is Successfully Saved");
                //LogActivity("EditTerm", string.Format("{0} | {1}| {2} ", t.PostType.Name, t.Title, t.Id));
                return RedirectToAction("Edit", new { id = termForm.Id });
            }
            PrepareAllTermsModel(termForm);
            return View(termForm);
        }

        public ActionResult Delete(Guid id)
        {
            var term = UOW.Terms.Get(id);
            if (term != null)
            {
                UOW.Terms.Delete(term.Id);
                AlertSuccessMessage("Term is Successfully Deleted");
                return RedirectToAction("Index");
            }
            return new HttpNotFoundResult();
        }

        private Term Map(TermForm form)
        {
            var dbterm = form.Id == Guid.Empty ? new Term() : UOW.Terms.Get(form.Id);
            if (dbterm == null)
                throw new Exception("no such Term");
            dbterm.Title = form.Title;
            dbterm.IsActive = form.IsActive;
            dbterm.IsPublic = form.IsPublic;
            dbterm.UrlKey = form.UrlKey;
            dbterm.DisplayOrder = form.DisplayOrder;
            dbterm.IncludeInTopMenu = form.IncludeInTopMenu;

            dbterm.ParentId = form.ParentId;
            if (dbterm.ParentId == Guid.Empty)
                dbterm.ParentId = null;
            dbterm.PostTypeId = form.PostTypeId;
            dbterm.PostType = UOW.PostTypes.Get(form.PostTypeId);
            dbterm.TaxonomyId = form.TaxonomyId;
            return dbterm;
        }

        private TermForm Map(Term term)
        {
            if (term == null)
                throw new Exception("no such Term");
            return new TermForm
            {
                Id = term.Id,
                PostType = UOW.PostTypes.Get(term.PostTypeId),
                Title = term.Title,
                Taxonomy = (TermTypeEnum)term.TaxonomyId,
                DisplayOrder = term.DisplayOrder,
                IsActive = term.IsActive,
                UrlKey = term.UrlKey,
                IsPublic = term.IsPublic,
                PostTypeId = term.PostTypeId,
                TaxonomyId = term.TaxonomyId,
                IncludeInTopMenu = term.IncludeInTopMenu,
                ParentId = term.ParentId
            };
        }

        public class TermModel
        {
            public TermModel()
            {
                List = new List<Term>();
            }

            public List<Term> List { get; set; }
            public int page { get; set; }
            public int size { get; set; }
            public TermTypeEnum Type { get; set; }
            public int total { get; set; }
            public int taxonomyid { get; set; }
            public string query { get; set; }
            public PostType PostType { get; set; }
        }
    }
}