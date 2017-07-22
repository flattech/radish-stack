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
                if(r.cols==null || r.cols.Count==0)
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
                        if(rows!=null)
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

        //private List<PageViewModel.ColViewModel> GenerateColumns(PageViewModel.RowViewModel row, PageViewModel.RowViewModel parent)
        //{
        //foreach (var col  in row.cols)
        //{
        //    PageViewModel.ColViewModel colview;
        //    if (parent == null)
        //    {
        //        colview = new PageViewModel.ColViewModel();
        //        colview.lg = col.lg;
        //        colview.text = col.text;

        //        colview.widgets = new List<PageViewModel.WidgetViewModel>();
        //    }
        //    else
        //    {
        //        colview = parent.cols.Add();
        //    }
        //    if (col.rows.Any())
        //    {
        //        foreach (var r in col.rows)
        //        {
        //            GenerateRow(r, col);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var w in col.widgets)
        //        {
        //            var model = _widgetrepository.Get(w.widgetid);
        //            if (model == null)
        //                continue;

        //            var widgetview = new PageViewModel.WidgetViewModel();

        //            widgetview.title = model.Title;
        //            widgetview.viewPath = model.ViewPath;

        //            var posts = new List<Post>();

        //            if (!string.IsNullOrEmpty(model.Posts))
        //                posts = _postrepository.GetByIds(model.Posts.ToGuids(), true).ToList();
        //            //else if (!string.IsNullOrEmpty(model.PostType))
        //            //    posts =
        //            //        _postrepository.GetByType(Guid.Parse(model.PostType), true, false, false,
        //            //            descerializedwidgets.PostCount == 0 ? 12 : model.PostCount).ToList();
        //            else if (!string.IsNullOrEmpty(model.Categories))
        //                posts = _postrepository.GetByIds(model.Categories.ToGuids(), true).ToList();
        //            else if (!string.IsNullOrEmpty(model.Tags))
        //                posts = _postrepository.GetByIds(model.Tags.ToGuids(), true).ToList();
        //            widgetview.posts = posts.Select(s => s.ToModel()).ToList();

        //            colview.widgets.Add(widgetview);
        //            //if (w.Widget.ReturnCategories)
        //            //{
        //            //    var cats = new List<Term>();
        //            //    if (!string.IsNullOrEmpty(w.Widget.PostType))
        //            //        cats =
        //            //            _termrepository.GetByPostType(Guid.Parse(w.Widget.PostType), (int)TermTypeEnum.Category)
        //            //                .ToList();

        //            //    widgetmodel.Categories = cats.Select(s => s.ToModel()).ToList();
        //            //}
        //            //if (w.Widget.ReturnTags)
        //            //{
        //            //    var tags = new List<Term>();
        //            //    if (!string.IsNullOrEmpty(w.Widget.PostType))
        //            //        tags =
        //            //            _termrepository.GetByPostType(Guid.Parse(w.Widget.PostType), (int)TermTypeEnum.Tag)
        //            //                .ToList();

        //            //    widgetmodel.Tags = tags.Select(s => s.ToModel()).ToList();
        //            //}
        //        }
        //        if(parent == null)
        //        row.cols.Add(colview);
        //        else
        //        {
        //            parent.rows.
        //        }
        //    }
        //}
        //return row.cols;
        //}
    }
}
