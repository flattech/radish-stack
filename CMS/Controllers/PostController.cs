using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS.Models;
using Core;
using Core.Extentions;
using Core.Repositories;
using Core.Repositories.Enums;
using Newtonsoft.Json;

namespace CMS.Controllers
{
    [Authorize]
    public class PostController : BaseController
    {
        private readonly PostRepository _repository = Pool.Instance.Posts;
        private object _settingsMediaLink;

        public PostController()
        {
            ViewBag.Current = "Post";
        }

        public ActionResult Index(Guid posttypeid, int page = 1, int size = 10, string query = "", Guid? termid = null)
        {
            var q = Query("PostTypeId", posttypeid.ToString()) + QueryLike("and Title", query);
            var model = new PostModel
            {
                PostType = UOW.PostTypes.Get(posttypeid),
                total = _repository.GetCount(q),
                page = page,
                query = query,
                size = size,
                List = _repository.GetAll(size, page, q).ToList()
            };
            PrepareAllTermsModel(model);
            return View(model);
        }

        public ActionResult Categories(Guid posttypeid)
        {
            return Json(UOW.Terms.GetByPostType(posttypeid).ToList()
                .Select(x => new { x.Id, x.Title }), JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public virtual void PrepareAllTermsModel(PostModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableTerms.Add(new SelectListItem
            {
                Text = "[None]",
                Value = "0"
            });
            var categories = UOW.Terms.GetTree(model.PostType.Id);
            foreach (var c in categories)
            {
                model.AvailableTerms.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(categories),
                    Value = c.Id.ToString()
                });
            }
        }

        public ActionResult Mini()
        {
            return PartialView("_mini", UOW.Posts.GetAll(20).ToList());
        }

        public ActionResult Create(Guid posttypeid)
        {
            var model = new PostForm
            {
                PostTypeId = posttypeid,
                CreationDate = DateTime.Now,
                PostType = UOW.PostTypes.Get(posttypeid)
            };
            InitMeta(model);
            PrepareAllTermsModel(model);
            return View(model);
        }

        [NonAction]
        public virtual void PrepareAllTermsModel(PostForm model)
        {
            var list = UOW.Terms.GetByPostType(model.PostTypeId);
            model.AvailableTerms =
                list.Where(x => x.TaxonomyId == (int)TermTypeEnum.Tree).OrderBy(x => x.DisplayOrder).ToList();
            model.AvailableTags = list.Where(x => x.TaxonomyId == (int)TermTypeEnum.Tag)
                .Select(x => x.Title).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var post = Map(form);
                    _repository.Save(post);
                    SyncTerms(form, post);
                    AlertSuccessMessage("Post successfully saved : " + form.Title);
                    LogActivity("CreateNewPost",
                        string.Format(" | {0} | {1} | {2} ", post.PostType.Title, post.Title, post.Title));
                    return RedirectToAction("Edit", new { post.Id });
                }
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
                LogException(ex);
            }
            form.PostType = UOW.PostTypes.Get(form.PostTypeId);
            InitMeta(form);
            AlertErrorMessage("error");
            return View(form);
        }

        private void LogException(Exception ex)
        {
            Logger.Fatal(this, "PostContoller | " + ex.Message);
        }

        private void LogActivity(string type, string message)
        {
            Logger.Info(this, type + " | " + message);
        }

        public ActionResult Edit(Guid id)
        {
            var post = _repository.GetObject(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var model = Map(post);
            PrepareAllTermsModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = Map(form);
                    _repository.Save(p);
                    SyncTerms(form, p);
                    AlertSuccessMessage("Post successfully saved : " + form.Title);
                    LogActivity("EditPost", string.Format("{0} | {1}| {2} ", p.PostType.Title, p.Title, p.Id));
                    return RedirectToAction("Edit", new { form.Id, posttypeid = form.PostTypeId });
                }
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
                LogException(ex);
            }
            form.PostType = UOW.PostTypes.Get(form.PostTypeId);
            PrepareAllTermsModel(form);
            return View(form);
        }

        public ActionResult Delete(Guid id, Guid posttypeid)
        {
            try
            {
                var target = UOW.Posts.Get(id);
                UOW.Posts.Delete(target.Id);
                UOW.Commit();
                AlertSuccessMessage("Post successfully Deleted : " + target.Title);
                return RedirectToAction("Index", new { posttypeid = posttypeid });
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
                return RedirectToAction("Index", new { posttypeid = posttypeid });
            }
        }

        private Post Map(PostForm form)
        {
            var post = form.Id == Guid.Empty ? new Post() : _repository.GetObject(form.Id);
            if (post == null)
                throw new Exception("no such Post");
            //@ToDo  should be date control
            post.PublishedDate = DateTime.UtcNow;

            post.Title = form.Title;
            if (string.IsNullOrEmpty(form.UrlKey) || (!string.IsNullOrEmpty(post.Title) && post.Title != form.Title))
                post.UrlKey = form.Title.GenerateSlug();
            post.ViewPath = form.ViewPath;
            post.Widgets = form.Widgets;
            post.Status = form.Status;
            post.Photo = form.Photo;
            post.CreationDate = form.CreationDate;
            post.UrlKey = form.UrlKey;
            post.Detail = form.Detail;
            post.PostTypeId = form.PostTypeId;
            post.PostType = UOW.PostTypes.Get(form.PostTypeId);
            if (post.PostType.EnableGallery)
                post.Gallery = form.Gallery;

            // SyncTerms(form.SelectedTerms, post);

            SyncAttachments(form, post);
            SyncMeta(post);
            return post;
        }

        private void InitMeta(PostForm model)
        {
            if (!string.IsNullOrEmpty(model.PostType.PostMetaFields))
                model.PostFields = JsonConvert.DeserializeObject<List<PostFields>>(model.PostType.PostMetaFields);

            if (!string.IsNullOrEmpty(model.PostType.PostMediaList))
                model.PostMedia = JsonConvert.DeserializeObject<List<PostFields>>(model.PostType.PostMediaList);
        }

        private void SyncMeta(Post post)
        {
            if (!string.IsNullOrEmpty(post.PostType.PostMetaFields))
            {
                var postfiels = JsonConvert.DeserializeObject<PostFields[]>(post.PostType.PostMetaFields);
                foreach (var pf in postfiels)
                {
                    pf.Value = Request.Form[$"PostMetaValues[{pf.Key}]"];
                }
                post.PostMetaValues = JsonConvert.SerializeObject(postfiels);
            }
        }
        private PostForm Map(Post post)
        {
            if (post == null)
                throw new Exception("no such Post");
            post.PostType = UOW.PostTypes.Get(post.PostTypeId);
            var form = new PostForm
            {
                SelectedTerms = post.PostTerms.Select(x => x.Term).Where(x => !x.IsTag()).Select(x => x.Id).ToList(),
                SelectedTags =
                    string.Join(",", post.PostTerms.Select(x => x.Term).Where(x => x.IsTag()).Select(x => x.Title)),
                Title = post.Title,
                Id = post.Id,
                CreationDate = post.CreationDate,
                Photo = post.Photo,
                Widgets = post.Widgets,
                Status = post.Status,
                IsActive = post.IsActive,
                UrlKey = post.UrlKey,
                ViewPath = post.ViewPath,
                PostType = UOW.PostTypes.Get(post.PostTypeId),
                PostTypeId = post.PostTypeId,
                Detail = post.Detail,
                MediaGallery = string.IsNullOrEmpty(post.Gallery) ? new List<MediaMini>() : JsonConvert.DeserializeObject<List<MediaMini>>(post.Gallery)
            };

            form.PostAttachments = FillPostMedia(post);
            FillPostMeta(form, post);
            return form;
        }

        private void FillPostMeta(PostForm form, Post post)
        {
            if (!string.IsNullOrEmpty(post.PostType.PostMetaFields))
            {
                var listposttype = JsonConvert.DeserializeObject<PostFields[]>(post.PostType.PostMetaFields);
                PostFields[] listpost = null;
                if (!string.IsNullOrEmpty(post.PostMetaValues))
                    listpost = JsonConvert.DeserializeObject<PostFields[]>(post.PostMetaValues);

                var finalList = new List<PostFields>();
                foreach (var field in listposttype)
                {
                    var exist = new PostFields { Key = field.Key };
                    if (listpost != null)
                        exist = listpost.SingleOrDefault(x => x.Key == field.Key) ?? exist;
                    finalList.Add(exist);
                }
                form.PostFields = finalList.ToList();
            }
        }

        private Dictionary<string, PostAttachment> FillPostMedia(Post post)
        {
            var dict = new Dictionary<string, PostAttachment>();
            if (string.IsNullOrEmpty(post.PostType.PostMediaList))
                return dict;

            var list = JsonConvert.DeserializeObject<List<PostAttachment>>(post.PostType.PostMediaList);

            var list2 = new List<PostAttachment>();
            if (!string.IsNullOrEmpty(post.Attachments))
                list2 = JsonConvert.DeserializeObject<List<PostAttachment>>(post.Attachments);

            foreach (var attachment in list)
            {
                var at = list2.SingleOrDefault(x => x.Key == attachment.Key) ?? new PostAttachment(attachment.Key);
                dict.Add(attachment.Key, at);
            }
            return dict;
        }

        private void SyncAttachments(PostForm form, Post post)
        {
            if (!string.IsNullOrEmpty(post.PostType.PostMediaList))
            {
                var postfields = JsonConvert.DeserializeObject<PostAttachment[]>(post.PostType.PostMediaList);
                foreach (var pf in postfields)
                {
                    var value = Request.Form[string.Format("Attachments[{0}].Value", pf.Key)];
                    if (value != null)
                    {
                        pf.Value = value;
                        pf.Id = Guid.Parse(Request.Form[string.Format("Attachments[{0}].Id", pf.Key)]);
                    }
                }
                post.Attachments = JsonConvert.SerializeObject(postfields);
            }
        }

        private void SyncTerms(PostForm form, Post post)
        {
            if (form.SelectedTerms == null)
                form.SelectedTerms = new List<Guid>();

            var tags = form.SelectedTags == null ? null : form.SelectedTags.Split(',', '،');
            var currenttags = tags == null ? null : UOW.Terms.GetAll(tags, post.PostTypeId);
            if (currenttags != null && currenttags.Any())
            {
                form.SelectedTerms = form.SelectedTerms.Union(currenttags.Select(x => x.Id).ToArray()).ToList();
            }

            var newtags = tags == null || currenttags == null ? tags : tags.Where(x => !currenttags.Select(z => z.Title).Contains(x));
            if (newtags != null)
                foreach (var nt in newtags)
                {
                    var term = new Term { Title = nt, TaxonomyId = (int)TermTypeEnum.Tag, PostTypeId = post.PostTypeId, IsActive = true, IsPublic = true };
                    UOW.Terms.Save(term);
                    form.SelectedTerms.Add(term.Id);
                }

            int i = 0;
            var currentterms = post.PostTerms;
            if (form.SelectedTerms != null)
                foreach (var t in form.SelectedTerms)
                {
                    i++;
                    var currentterm = currentterms.SingleOrDefault(x => x.TermId == t);
                    if (currentterm == null)
                    {
                        UOW.PostTerms.Save(new PostTerm { TermId = t, PostId = post.Id, DisplayOrder = i });
                    }
                    else
                    {
                        currentterm.DisplayOrder = i;
                        UOW.PostTerms.Update(currentterm);
                    }
                }

            if (currentterms != null)
                foreach (var t in currentterms)
                {
                    var ex = form.SelectedTerms != null && form.SelectedTerms.Contains(t.TermId);
                    if (!ex)
                        UOW.PostTerms.Delete(t.Id);
                }
        }


        public class StatusView
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public class PostView
        {
            public DateTime CreationDate { get; set; }
            public string Title { get; set; }
            public string StatusName { get; set; }
            public int Status { get; set; }
            public string Image { get; set; }
            public Guid Id { get; set; }
            public Guid PostTypeId { get; set; }
            public string Terms { get; set; }
            public string TermIds { get; set; }
        }
    }

    public static class TermExtensions
    {
        public static bool IsTag(this Term Term)
        {
            return Term.TaxonomyId == (int)TermTypeEnum.Tag;
        }
    }

    public class PostModel
    {
        public PostModel()
        {
            AvailableTerms = new List<SelectListItem>();
            List = new List<Post>();
        }

        public List<Post> List { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public int total { get; set; }
        public string query { get; set; }
        public IList<SelectListItem> AvailableTerms { get; set; }
        public PostType PostType { get; set; }
        public Guid? TermId { get; set; }
    }
}