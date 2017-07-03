using System;
using System.Linq;
using System.Web.Mvc;
using CMS.Models;
using Core.Repositories;
using Core.Repositories.Enums;


namespace CMS.Controllers
{
    [Authorize]
    public class WidgetController : BaseController
    {
        public WidgetController()
        {
            ViewBag.Current = "Widget";
        }

        public ActionResult Index()
        {
            var list = UOW.Widgets.GetAll().ToList();
            return View(list);
        }

        public ActionResult Mini()
        {
            return PartialView("_mini", UOW.Widgets.GetAll().ToList());
        }

        public ActionResult Create()
        {
            var model = new WidgetForm();
            PrepareAllTermsModel(model);
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            Widget widget = UOW.Widgets.Get(id);
            if (widget == null)
            {
                return HttpNotFound();
            }
            var model = Map(widget);

            PrepareAllTermsModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WidgetForm form)
        {
            if (ModelState.IsValid)
            {
                var t = Map(form);
                UOW.Widgets.Save(t);
                AlertSuccessMessage("Widget is Successfully Saved");
                return RedirectToAction("Edit", new { id = t.Id });
            }
            PrepareAllTermsModel(form);
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WidgetForm form)
        {
            if (ModelState.IsValid)
            {
                var t = Map(form);
                UOW.Widgets.Update(t);
                AlertSuccessMessage("Widget is Successfully Saved");
                return RedirectToAction("Edit", new { id = form.Id });
            }
            PrepareAllTermsModel(form);
            return View(form);
        }

        private Widget Map(WidgetForm form)
        {
            var dbWidget = form.Id == Guid.Empty ? new Widget() : UOW.Widgets.Get(form.Id);
            if (dbWidget == null)
                throw new Exception("no such Widget");
            dbWidget.Title = form.Title;
            dbWidget.IsActive = form.IsActive;
            dbWidget.PostCount = form.PostCount;


            dbWidget.PostType = form.PostType ?? "";
            dbWidget.Tags = form.SourceTags == null ? null : string.Join(",", form.SourceTags.ToArray());
            dbWidget.ReturnTags = form.ReturnTags;
            dbWidget.Categories = form.SourceCategories == null ? null : string.Join(",", form.SourceCategories.ToArray());
            dbWidget.ReturnCategories = form.ReturnCategories;
            dbWidget.ReturnPosts = form.ReturnPosts;
            dbWidget.Posts = form.SourcePosts;

            dbWidget.ViewPath = form.ViewPath;
            return dbWidget;
        }

        [NonAction]
        public virtual void PrepareAllTermsModel(WidgetForm model)
        {
            var posttypes = UOW.PostTypes.GetAll();
            model.AvailablePostTypes.Add(new SelectListItem { Text = "[None]", Value = "" });
            foreach (var c in posttypes)
            {

                model.AvailablePostTypes.Add(new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                });
            }

            var list = UOW.Terms.GetAll(TermTypeEnum.Category);
            foreach (var c in list)
            {
                model.AvailableTerms.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(list),
                    Value = c.Id.ToString()
                });
            }
            foreach (var c in UOW.Terms.GetAll(TermTypeEnum.Tag))
            {
                model.AvailableTags.Add(new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                });
            }

            if (!string.IsNullOrEmpty(model.SourcePosts))
            {
                var list2 =UOW.Posts.
                    GetByIds(model.SourcePosts.Split(Convert.ToChar(","))
                    .Select(Guid.Parse).ToArray(), true);
                model.AvailablePosts = list2.Select(x => new PostView
                {
                    Id = x.Id,
                    Title = x.Title,
                    Photo = x.Photo
                }).ToList();
            }
        }

        private WidgetForm Map(Widget widget)
        {
            if (widget == null)
                throw new Exception("no such Widget");

            var model = new WidgetForm();

            model.Id = widget.Id;
            model.Title = widget.Title;
            model.IsActive = widget.IsActive;
            model.ViewPath = widget.ViewPath;
            model.PostCount = widget.PostCount;            
            model.PostType = widget.PostType;
            model.SourcePosts = widget.Posts;
            model.ReturnPosts = widget.ReturnPosts;
            model.ReturnCategories = widget.ReturnCategories;
            model.ReturnTags = widget.ReturnTags;
            model.SourceTags = string.IsNullOrEmpty(widget.Tags) ? null : widget.Tags.Split(Convert.ToChar(",")).ToList();
            model.SourceCategories = string.IsNullOrEmpty(widget.Categories) ? null : widget.Categories.Split(Convert.ToChar(",")).ToList();


            return model;
        }
    }
}