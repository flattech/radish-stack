using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Core.Repositories;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var page = Pool.Instance.Posts.GetPage(Guid.Parse("86BCA7C3-7F4A-4DCF-A39D-2D6B7629C934"));

            var menulist = Pool.Instance.Menues.GetTree().ToList();

            if (page == null)
                return new ContentResult { Content = "Add Home Page to DB" };

            var model = new PageViewModel
            {
                rows = FillWidgetsDatasource(page),
                menus = FillMenuDataSource(menulist)
            };
            //var model = FillWidgetsDatasource(page);
            return View(model);
        }
    }
}