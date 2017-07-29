using System;
using System.Web.Mvc;
using Core;

namespace Web.Controllers
{
    public class WidgetController : BaseController
    {
        public ActionResult Render(Guid id)
        {
            var widget = Pool.Instance.Widgets.Get(id);
            var model = FillWidgetData(widget);
            return PartialView(widget.ViewPath, model);
        }

        public JsonResult GetJson(Guid id)
        {
            var widget = Pool.Instance.Widgets.Get(id);
            var model = FillWidgetData(widget);
            return Json(model);
        }
    }
}