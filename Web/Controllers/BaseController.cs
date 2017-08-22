using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Core.Extentions;
using Core.Repositories;
using Core.Repositories.Enums;
using Newtonsoft.Json;
using Web.Extentions;
using Web.Models.Post;

namespace Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private PostRepository _postrepository = Pool.Instance.Posts;
        private WidgetRepository _widgetrepository = Pool.Instance.Widgets;
        private TermRepository _termrepository = Pool.Instance.Terms;

        public List<PageViewModel.RowViewModel> FillWidgetsDatasource(Post p)
        {
            //return EngineContext.Current.Resolve<ICacheManager>().Get("widgets",() =>
            //{
            var descerializedwidgets = JsonConvert.DeserializeObject<PageViewModel>(p.Widgets);

            var widgetmodel = new List<PageViewModel.RowViewModel>();

            foreach (var r in descerializedwidgets.rows)
            {
                if (r.cols == null || r.cols.Count == 0)
                    continue;

                var generatedRow = GenerateRow(r);

                if (generatedRow != null)
                    widgetmodel.Add(generatedRow);

            }
            return widgetmodel;
            //});
        }

        public PageViewModel.WidgetViewModel FillWidgetData(Widget w)
        {
            var widgetview = new PageViewModel.WidgetViewModel
            {
                title = w.Title,
                viewPath = w.ViewPath
            };

            if (w.ReturnPosts)
            {
              
                var posts = _postrepository.GetPosts(new PostSearch
                {
                    PostIds = !string.IsNullOrEmpty(w.Posts) ? w.Posts.ToGuids() : null,
                    PostTypeId = !string.IsNullOrEmpty(w.PostType) ? Guid.Parse(w.PostType) : (Guid?)null,
                    Status = PostSatusEnum.Published
                }, 1, w.PostCount).ToList();

                //else if (!string.IsNullOrEmpty(w.Categories))
                //    posts = _postrepository.GetByIds(w.Categories.ToGuids(), true).ToList();
                //else if (!string.IsNullOrEmpty(w.Tags))
                //    posts = _postrepository.GetByIds(w.Tags.ToGuids(), true).ToList();
                widgetview.posts = posts.Select(s => s.ToDetailsModel(true,false)).ToList();
            }
            return widgetview;
            //if (w.Widget.ReturnCategories)
            //{
            //    var cats = new List<Term>();
            //    if (!string.IsNullOrEmpty(w.Widget.PostType))
            //        cats =
            //            _termrepository.GetByPostType(Guid.Parse(w.Widget.PostType), (int)TermTypeEnum.Category)
            //                .ToList();

            //    widgetmodel.Categories = cats.Select(s => s.ToModel()).ToList();
            //}
            //if (w.Widget.ReturnTags)
            //{
            //    var tags = new List<Term>();
            //    if (!string.IsNullOrEmpty(w.Widget.PostType))
            //        tags =
            //            _termrepository.GetByPostType(Guid.Parse(w.Widget.PostType), (int)TermTypeEnum.Tag)
            //                .ToList();

            //    widgetmodel.Tags = tags.Select(s => s.ToModel()).ToList();
            //}
        }

        private PageViewModel.RowViewModel GenerateRow(PageViewModel.RowViewModel row)
        {

            var rowview = new PageViewModel.RowViewModel { classname = row.classname };
            foreach (var col in row.cols)
            {
                var colview = new PageViewModel.ColViewModel
                {
                    lg = col.lg,
                    text = col.text,
                    classname = col.classname
                };

                if (col.rows.Any())
                {
                    foreach (var r in col.rows)
                    {
                        var rows = GenerateRow(r);
                        if (rows != null)
                            colview.rows.Add(rows);
                    }
                }
                else
                {
                    foreach (var w in col.widgets)
                    {
                        var widget = _widgetrepository.Get(w.widgetid);
                        if (widget == null)
                            continue;
                        var widgetview = FillWidgetData(widget);
                        colview.widgets.Add(widgetview);
                    }
                }
                rowview.cols.Add(colview);
            }
            return rowview;
        }

    }
}
