using System;
using System.Linq;
using System.Web.Mvc;
using Core;
using Web.Extentions;

namespace Web.Controllers
{
    public class PostController : BaseController
    {
        public ActionResult Details(Guid id)
        {

            var item = Pool.Instance.Posts.GetObject(id);
               if (item == null)
                   return null;
               var model = item.ToDetailsModel(true,true);

               //if (item.PostWidgets.Any())
              //     model.Widgets = FillWidgetsDatasource(item);

               //var posttype = item.PostType;
               //var terms = _termRepository.GetByPostType(posttype.Id);
               //model.Categories = terms.Where(x => !x.IsTag()).OrderBy(x => x.DisplayOrder).Select(x => x.ToMiniModel()).Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
               //model.Tags = terms.Where(x => x.IsTag()).Select(x => x.ToMiniModel()).Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
               //model.RecentPosts = _service.GetByType(item.PostType.Id, true).Take(4).Select(x => x.ToModel()).Where(x => !string.IsNullOrEmpty(x.Title)).ToList();

               return View(model.TemplateViewPath, model);
        }
    }
}