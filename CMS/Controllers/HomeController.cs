using System.Web.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController()
        {
            ViewBag.Current = "Home";
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Refresh()
        {
            AppResources.Instance.Refresh();
            return RedirectToAction("Index", "Home");
        }
    }
}
