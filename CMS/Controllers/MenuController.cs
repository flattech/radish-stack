﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS.Models;
using Core.Repositories;


namespace CMS.Controllers
{
    [Authorize]
    public class MenuItemController : BaseController
    {
        public MenuItemController( )
        {
            ViewBag.Current = "MenuItem";
        }

        public ActionResult Index()
        {
            var list = UOW.Menues.GetTree().ToList();
            var model = new MenuItemViewModel
            {
                List = list.Select(x => new MenuItem { Id = x.Id, Title = x.GetFormattedMenuBreadCrumb(list, "--") }).ToList()
            };
            return View(model);
        }
        public class MenuItemViewModel
        {
            public IList<MenuItem> List { get; set; }
        }

        public ActionResult Create()
        {
            var model = new MenuItemForm
            {
                IsActive = true,
            };
            PrepareAllMenuItemsModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItemForm form)
        {
            if (ModelState.IsValid)
            {
                var model = Map(form);
                UOW.Menues.Save(model);
                AlertSuccessMessage("MenuItem is Successfully Saved");

                return RedirectToAction("Edit", new { id = model.Id });
            }
            PrepareAllMenuItemsModel(form);
            return View(form);
        }

        [NonAction]
        public virtual void PrepareAllMenuItemsModel(MenuItemForm model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableMenuItems.Add(new SelectListItem
            {
                Text = null,
                Value = null
            });
            var items = UOW.Menues.GetTree().ToList();
            foreach (var c in items)
            {
                model.AvailableMenuItems.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(items),
                    Value = c.Id.ToString()
                });
            }
        }

        public ActionResult Edit(Guid id)
        {
            MenuItem postcategory = UOW.Menues.Get(id);
            if (postcategory == null)
            {
                return HttpNotFound();
            }
            var model = Map(postcategory);
            PrepareAllMenuItemsModel(model);
            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuItemForm MenuItemForm)
        {
            if (ModelState.IsValid)
            {
                var t = Map(MenuItemForm);
                UOW.Menues.Update(t);
                UOW.Commit();
                AlertSuccessMessage("MenuItem is Successfully Saved");
                return RedirectToAction("Edit", new { id = MenuItemForm.Id });
            }
            PrepareAllMenuItemsModel(MenuItemForm);
            return View(MenuItemForm);
        }

     
        public ActionResult Delete(Guid id)
        {
            MenuItem MenuItem = UOW.Menues.Get(id);
            if (MenuItem != null)
            {
                UOW.Menues.Delete(MenuItem.Id);
                UOW.Commit();
                AlertSuccessMessage("MenuItem is Successfully Deleted");
                return RedirectToAction("Index");
            }
            return new HttpNotFoundResult();
        }


        private MenuItem Map(MenuItemForm form)
        {
            var dbMenuItem = form.Id == Guid.Empty ? new MenuItem() : UOW.Menues.Get(form.Id);
            if (dbMenuItem == null)
                throw new Exception("no such MenuItem");
            dbMenuItem.Title = form.Title;
            dbMenuItem.IsActive = form.IsActive;
            dbMenuItem.DisplayOrder = form.DisplayOrder;
            dbMenuItem.EntityId = form.EntityId;
            dbMenuItem.EntityName = form.EntityName;
            dbMenuItem.IncludeInFooter = form.IncludeInFooter;
            dbMenuItem.IncludeInHeader = form.IncludeInHeader;
            dbMenuItem.Url = form.Url;
            dbMenuItem.ParentId = form.ParentId;
            return dbMenuItem;
        }
        private MenuItemForm Map(MenuItem menuItem)
        {
            if (menuItem == null)
                throw new Exception("no such MenuItem");
            var model = new MenuItemForm
            {
                Id = menuItem.Id,
                Title = menuItem.Title,
                EntityName = menuItem.EntityName,
                IncludeInFooter = menuItem.IncludeInFooter,
                IncludeInHeader = menuItem.IncludeInHeader,
                EntityId = menuItem.EntityId,
                DisplayOrder = menuItem.DisplayOrder,
                Url = menuItem.Url,
                IsActive = menuItem.IsActive,
                ParentId = menuItem.ParentId,
            };
            return model;
        }
    }
}