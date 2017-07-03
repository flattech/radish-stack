using System;
using System.Web.Mvc;
using Core;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
       
        }
        public ActionResult Index()
        {
            var page = Pool.Instance.Posts.Get(Guid.Parse("86bca7c3-7f4a-4dcf-a39d-2d6b7629c934"));
          
            if (page == null)
                return new ContentResult {Content = "Add Home Page to DB"};

            var model = new PageViewModel
            {
             //   Widgets = FillWidgetsDatasource(page)
            };

            return View(model);
        }
    }
}