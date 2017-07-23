using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Core;
using Core.Extentions;
using Core.Repositories;
using Core.Repositories.Enums;
using Web.Extentions;
using Web.Models;
using MenuItem = System.Web.UI.WebControls.MenuItem;
using Rows = Web.Models.Rows;

namespace Web.Controllers
{

    //[NopHttpsRequirement(SslRequirement.NoMatter)]
    //[WwwRequirement]
    public abstract partial class BaseController : Controller
    {

        private PostRepository _postrepository = Pool.Instance.Posts;
        private WidgetRepository _widgetrepository = Pool.Instance.Widgets;
        private TermRepository _termrepository = Pool.Instance.Terms;

        //protected virtual ActionResult InvokeHttp404()
        //{
        //    // Call target Controller and pass the routeData.
        //    IController errorController = new CommonController();

        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", "Common");
        //    routeData.Values.Add("action", "PageNotFound");

        //    errorController.Execute(new RequestContext(this.HttpContext, routeData));

        //    return new EmptyResult();
        //}

        public List<PageViewModel.RowViewModel> FillWidgetsDatasource(Post p)
        {
            //return EngineContext.Current.Resolve<ICacheManager>().Get("Mentis.widgets",() =>
            //{

            var descerializedwidgets = Newtonsoft.Json.JsonConvert.DeserializeObject<PageViewModel>(p.Widgets);


            //foreach (var w in p.PostWidgets.OrderBy(x => x.DisplayOrder))
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

        private PageViewModel.RowViewModel GenerateRow(PageViewModel.RowViewModel row)
        {

            var rowview = new PageViewModel.RowViewModel();
            rowview.classname = row.classname;
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
                        var model = _widgetrepository.Get(w.widgetid);
                        if (model == null)
                            continue;

                        var widgetview = new PageViewModel.WidgetViewModel
                        {
                            title = model.Title,
                            viewPath = model.ViewPath
                        };


                        var posts = new List<Post>();

                        if (!string.IsNullOrEmpty(model.Posts))
                            posts = _postrepository.GetByIds(model.Posts.ToGuids(), true).ToList();
                        //else if (!string.IsNullOrEmpty(model.PostType))
                        //    posts =
                        //        _postrepository.GetByType(Guid.Parse(model.PostType), true, false, false,
                        //            descerializedwidgets.PostCount == 0 ? 12 : model.PostCount).ToList();
                        else if (!string.IsNullOrEmpty(model.Categories))
                            posts = _postrepository.GetByIds(model.Categories.ToGuids(), true).ToList();
                        else if (!string.IsNullOrEmpty(model.Tags))
                            posts = _postrepository.GetByIds(model.Tags.ToGuids(), true).ToList();
                        widgetview.posts = posts.Select(s => s.ToModel()).ToList();

                        colview.widgets.Add(widgetview);
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
                }
                rowview.cols.Add(colview);
            }
            return rowview;
        }


        public List<Core.Repositories.MenuItem> FillMenuDataSource(IList<Core.Repositories.MenuItem> menu)
        {
            var groups = menu.GroupBy(i => i.ParentId);

            var enumerable = groups as IList<IGrouping<Guid?, Core.Repositories.MenuItem>> ?? groups.ToList();
            var first = enumerable.FirstOrDefault(g => g.Key.HasValue == false);
            if (first == null)
                return null;
            {
                var roots = first.ToList();

                if (roots.Count <= 0)
                    return roots;
                var dict = enumerable.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                foreach (Core.Repositories.MenuItem t in roots)
                    AddChildren(t, dict);
                return roots;
            }
        }

        private static void AddChildren(Core.Repositories.MenuItem node, IDictionary<Guid, List<Core.Repositories.MenuItem>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Children = source[node.Id];
                foreach (Core.Repositories.MenuItem t in node.Children)
                    AddChildren(t, source);
            }
            else
            {
                node.Children = new List<Core.Repositories.MenuItem>();
            }
        }
    }
}
