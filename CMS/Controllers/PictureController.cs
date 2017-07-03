using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Core.Repositories;

namespace CMS.Controllers
{
    [Authorize]
    public class PictureController : BaseController
    {
        public PictureController()
        {
        }

        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/content/uploaded/")); } //Path should! always end with '/'
        }

        [HttpPost]
        //do not validate request token (XSRF)
        public ActionResult AsyncUpload()
        {

            //we process it distinct ways based on a browser
            //find more info here http://stackoverflow.com/questions/4884920/mvc3-valums-ajax-file-upload
            Stream stream = null;
            var fileName = "";
            var contentType = "";
            HttpPostedFileBase postedFile = Request.Files[0];

            if (String.IsNullOrEmpty(Request["qqfile"]))
            {
                // IE
                if (postedFile == null)
                    throw new ArgumentException("No file uploaded");
                stream = postedFile.InputStream;
                fileName = Path.GetFileName(postedFile.FileName);
                contentType = postedFile.ContentType;
            }
            else
            {
                //Webkit, Mozilla
                stream = Request.InputStream;
                fileName = Request["qqfile"];
            }

            var fileExtension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            fileName = Guid.NewGuid() + fileExtension;
            string filepath = string.Format("/content/uploaded/" + "{0}", fileName);
            var relativePath = string.Format(StorageRoot + "{0}", fileName);
            postedFile.SaveAs(relativePath);
            //var fileBinary = new byte[stream.Length];
            //stream.Read(fileBinary, 0, fileBinary.Length);


            var media = new Media
            {
                RelativePath = filepath,
                CreationDate = DateTime.UtcNow,
                //LastModified = DateTime.UtcNow
            };
            UOW.Medias.Save(media);
            UOW.Commit();
            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Json(new
            {
                success = true,
                pictureId = media.Id,
                imageUrl = media.RelativePath
            },
               "text/plain");
        }
    }
}
