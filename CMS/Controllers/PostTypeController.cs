using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Core.Extentions;
using Core.Repositories;


namespace CMS.Controllers
{
    [Authorize]
    public class PostTypeController : BaseController
    {
        private readonly PostTypeRepository _repository = Pool.Instance.PostTypes;

        public PostTypeController()
        {
            ViewBag.Current = "PostType";
            ViewBag.CurrentSection = "Admin";
        }

        public ActionResult Index(int page = 1, int size = 10, string query = "", string order = "title", string sort = "asc")
        {
            var q = QueryLike("Title", query);
            var ordering = "";
            if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(sort))
                ordering = order + " " + sort;

            var model = new PostTypeModel
            {
                total = _repository.GetCount(q),
                page = page,
                query = query,
                size = size,
                order = order,
                sort = sort,
                List = _repository.GetAll(size, page, q, ordering).ToList()
            };
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new PostType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostType form)
        {
            try
            {
                if (string.IsNullOrEmpty(form.Title) || string.IsNullOrEmpty(form.UrlKey))
                    ModelState.AddModelError("", "Title and UrlKey are Required");
                if (ModelState.IsValid)
                {
                    _repository.Save(form);
                    AlertSuccessMessage(AppResources.Instance.Get("SuccessfullySaved") + " " + form.Title);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
            }
            return View(form);
        }

        public ActionResult Edit(Guid id)
        {

            return View(_repository.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostType form)
        {
            try
            {
                if(string.IsNullOrEmpty(form.Title) || string.IsNullOrEmpty(form.UrlKey))
                    ModelState.AddModelError("","Title and UrlKey are Required");

                if (ModelState.IsValid)
                {
                    _repository.Save(form);
                    AlertSuccessMessage(AppResources.Instance.Get("SuccessfullySaved") + " " + form.Title);
                    return RedirectToAction("Edit", new { form.Id });
                }
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
            }
            return View(form);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                var item = _repository.Get(id);
                _repository.Delete(item.Id);
                AlertSuccessMessage(AppResources.Instance.Get("SuccessfullySaved") + " " + item.Title);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
    public class PostTypeModel
    {

        public PostTypeModel()
        {
            List = new List<PostType>();
        }
        public List<PostType> List { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public int total { get; set; }
        public string query { get; set; }
        public string order { get; set; }
        public string sort { get; set; }
    }
}