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

        public List<PageViewModel.WidgetViewModel> FillWidgetsDatasource(Post p)
        {
            //return EngineContext.Current.Resolve<ICacheManager>().Get("Mentis.widgets",() =>
            //{

            var descerializedwidgets = Newtonsoft.Json.JsonConvert.DeserializeObject<Rows>(p.Widgets);
            var widgets = new List<PageViewModel.WidgetViewModel>();
            var _postrepository = Pool.Instance.Posts;
            var _widgetrepository = Pool.Instance.Widgets;
            var _termrepository = Pool.Instance.Terms;

            //foreach (var w in p.PostWidgets.OrderBy(x => x.DisplayOrder))
            foreach (var r in descerializedwidgets.rows)
            {
                foreach (var col in r.cols)
                {
                    foreach (var w in col.widgets)
                    {
                        var widgetmodel = new PageViewModel.WidgetViewModel();

                        var model = _widgetrepository.Get(w.widgetid);
                        if (model == null)
                            continue;

                        widgetmodel.Title = model.Title;
                        widgetmodel.ViewPath = model.ViewPath;

                        var posts = new List<Post>();

                        if (!string.IsNullOrEmpty(model.Posts))
                            posts = _postrepository.GetByIds(model.Posts.ToGuids(), true).ToList();
                        else if (!string.IsNullOrEmpty(model.PostType))
                            posts =
                                _postrepository.GetByType(Guid.Parse(model.PostType), true, false, false,
                                    descerializedwidgets.PostCount == 0 ? 12 : model.PostCount).ToList();
                        else if (!string.IsNullOrEmpty(model.Categories))
                            posts = _postrepository.GetByIds(model.Categories.ToGuids(), true).ToList();
                        else if (!string.IsNullOrEmpty(model.Tags))
                            posts = _postrepository.GetByIds(model.Tags.ToGuids(), true).ToList();
                        widgetmodel.Posts = posts.Select(s => s.ToModel()).ToList();

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
                        widgets.Add(widgetmodel);
                    }
                }
            }
            return widgets;
            //});
        }

    }
}
