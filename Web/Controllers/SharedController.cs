using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Core.Repositories;
using Web.Models;

namespace Web.Controllers
{
    public class SharedController : BaseController
    {
        [ChildActionOnly]
        public ActionResult TopMenu()
        {

            var menulist = Pool.Instance.Menues.GetAll().ToList();

            var model = new TopMenuModel
            {
                MenuItems = FillMenuDataSource(menulist)
            };
            return PartialView(model);
        }
        [NonAction]
        protected virtual IList<MenuItem> FillMenuDataSource(IList<MenuItem> list, Guid? rootId = null)
        {
            var result = new List<MenuItem>();

            var items = list.Where(c => c.ParentId == rootId).ToList();
            foreach (var item in items)
            {
                if (item.EntityId != null)
                {
                    //set icon
                }
                item.Children.AddRange(FillMenuDataSource(list, item.Id));
                result.Add(item);
            }
            return result;
        }
    }
}