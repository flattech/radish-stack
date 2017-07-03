using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace CMS.Controllers
{
    /// <summary>
    /// Controller used by jbimages (JustBoil.me) plugin (TimyMCE)
    /// </summary>
    //do not validate request token (XSRF)
    public partial class JbimagesController : BaseController
    {
        //private readonly WebHelper _webHelper;

        //public JbimagesController(IWebHelper webHelper)
        //{
        //    this._webHelper = webHelper;
        //}

        [NonAction]
        protected virtual IList<string> GetAllowedFileTypes()
        {
            return new List<string> { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
        }
        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/content/uploaded/")); } //Path should! always end with '/'
        }
        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count == 0)
                throw new Exception("No file uploaded");

            var uploadFile = Request.Files[0];
            if (uploadFile == null)
            {
                ViewData["resultCode"] = "failed";
                ViewData["result"] = "No file name provided";
                return View();
            }

            var fileName = Path.GetFileName(uploadFile.FileName);
            if (String.IsNullOrEmpty(fileName))
            {
                ViewData["resultCode"] = "failed";
                ViewData["result"] = "No file name provided";
                return View();
            }

            var relativePath = string.Format(StorageRoot + "{0}", fileName);

            var fileExtension = Path.GetExtension(fileName);
            if (!GetAllowedFileTypes().Contains(fileExtension.ToLower()))
            {
                ViewData["resultCode"] = "failed";
                ViewData["result"] = string.Format("Files with {0} extension cannot be uploaded", fileExtension);
                return View();
            }

            uploadFile.SaveAs(relativePath);
           // string mediahost = ConfigurationManager.AppSettings["CMSUrl"];
            ViewData["resultCode"] = "success";
            ViewData["result"] = "success";
            ViewData["filename"] = this.Url.Content(string.Format("{0}{1}", "/content/uploaded/", fileName));
            return View();
        }
    }
}
