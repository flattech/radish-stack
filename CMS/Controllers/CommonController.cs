using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Repositories;

namespace CMS.Controllers
{
    [Authorize]
    public class CommonController : BaseController
    {

        public CommonController()
        {

        }
        public ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;

            return View();
        }
        public class MenuModel
        {
            public IList<PostType> PostTypes { get; set; }
            public int PostTypeId { get; set; }
        }
        public ActionResult SideBarMenu(int posttypeid = 0)
        {
            var ct = UOW.PostTypes.GetAll().ToList();
            var model = new MenuModel
            {
                PostTypes = ct,
                PostTypeId = posttypeid
            };
            return PartialView(model);
        }
    }
}