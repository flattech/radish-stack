using System;
using System.Linq;
using System.Web.Mvc;
using CMS.Models;
using Core.Repositories;


namespace CMS.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController()
        {
            ViewBag.Current = "User";
            ViewBag.CurrentSection = "Admin";
        }

        public ActionResult Index()
        {
            return View(UOW.Users.GetAll().ToList());
        }

        public ActionResult Create()
        {
            return View(new UserForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserForm form)
        {
            if (!IsAdmin)
            {
                return HttpNotFound();
            }
            if (UOW.Users.GetByUsername(form.UserName) != null)
            {
                ModelState.AddModelError("", "User name already exists");
            }
            if (ModelState.IsValid)
            {
                var app = Map(form);
                UOW.Users.Add(app);
                UOW.Commit();
                AlertSuccessMessage("Successfully saved : " + form.UserName);
                return RedirectToAction("Index");
            }
            AlertErrorMessage("Error");
            return View(form);
        }

        public ActionResult Check(string username)
        {
            var user = UOW.Users.GetByUsername(username);
            if (user == null)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            var user = UOW.Users.Get(id);
            if (user == null || (!IsAdmin))
            {
                return HttpNotFound();
            }
            return View(Map(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserForm form)
        {
            if (!IsAdmin)
            {
                return HttpNotFound();
            }
            var existing = UOW.Users.GetByUsername(form.UserName);
            if (existing != null && existing.Id != form.Id)
            {
                ModelState.AddModelError("", "User name already exists");
            }
            if (ModelState.IsValid)
            {
                var app = Map(form);
                UOW.Users.Update(app);
                UOW.Commit();
                AlertSuccessMessage("User successfully saved : " + form.UserName);
                return RedirectToAction("Index");
            }
            return View(form);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                var target = UOW.Users.Get(id);
                UOW.Users.Delete(target.Id);
                UOW.Commit();
                AlertSuccessMessage("User successfully Deleted : " + target.Username);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
                return RedirectToAction("Index");
            }
        }

        private User Map(UserForm form)
        {
            var dbtarget = form.Id == Guid.Empty ? new User() : UOW.Users.Get(form.Id);
            if (dbtarget == null)
                throw new Exception("no such User");
            dbtarget.Name = form.Name;
            dbtarget.Username = form.UserName;
            dbtarget.SetPassword(form.Password);

            dbtarget.Email = "Email";
            dbtarget.Role = form.Role;
            return dbtarget;
        }

        private UserForm Map(User user)
        {
            if (user == null)
                throw new Exception("no such User");
            return new UserForm
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}