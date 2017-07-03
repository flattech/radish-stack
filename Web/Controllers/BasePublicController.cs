using System.Web.Mvc;
using System.Web.Routing;

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

        //public List<PageViewModel.WidgetViewModel> FillWidgetsDatasource(Post p)
        //{
        //    //return EngineContext.Current.Resolve<ICacheManager>().Get("Mentis.widgets",() =>
        //    //{
        //    var widgets = new List<PageViewModel.WidgetViewModel>();
        //    var _postrepository = Pool.Instance.Posts;
        //    var _termrepository = Pool.Instance.Terms;

        //    foreach (var w in p.PostWidgets.OrderBy(x => x.DisplayOrder))
        //    {
        //        var widgetmodel = new PageViewModel.WidgetViewModel();
        //        if (w == null)
        //            return null;

        //        widgetmodel.Title = w.Widget.Title;
        //        widgetmodel.ViewPath = w.Widget.ViewPath;
        //        widgetmodel.Location = w.Location;
        //        var config = Newtonsoft.Json.JsonConvert.DeserializeObject<WidgetConfig>(w.Widget.Config);

        //        if (config.ReturnPosts)
        //        {
        //            var posts = new List<Post>();

        //            if (!string.IsNullOrEmpty(config.Posts))
        //                posts = _postrepository.GetByIds(config.Posts.ToIntegers(), true);
        //            else if (!string.IsNullOrEmpty(config.PostType))
        //                posts =
        //                    _postrepository.GetByType(Convert.ToInt32(config.PostType), true,false,false,w.Widget.PostCount == 0 ? 12 : w.Widget.PostCount).ToList();
        //            else if (!string.IsNullOrEmpty(config.Categorys))
        //                posts = _postrepository.GetByIds(config.Categorys.ToIntegers(), true);
        //            else if (!string.IsNullOrEmpty(config.Tags))
        //                posts = _postrepository.GetByIds(config.Tags.ToIntegers(), true);
        //            widgetmodel.Posts = posts.Select(s => s.ToModel()).ToList();
        //        }
        //        if (config.ReturnCategorys)
        //        {
        //            var cats = new List<Term>();
        //            if (!string.IsNullOrEmpty(config.PostType))
        //                cats =
        //                    _termrepository.GetByPostType(Convert.ToInt32(config.PostType), (int)TermTypeEnum.Category)
        //                        .ToList();

        //            widgetmodel.Categories = cats.Select(s => s.ToModel()).ToList();
        //        }
        //        if (config.ReturnTags)
        //        {
        //            var tags = new List<Term>();
        //            if (!string.IsNullOrEmpty(config.PostType))
        //                tags =
        //                    _termrepository.GetByPostType(Convert.ToInt32(config.PostType), (int)TermTypeEnum.Tag)
        //                        .ToList();

        //            widgetmodel.Tags = tags.Select(s => s.ToModel()).ToList();
        //        }
        //        widgets.Add(widgetmodel);
        //    }
        //    return widgets;
        //    //});
        //}

    }
}
