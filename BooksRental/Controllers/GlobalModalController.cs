using System.Collections.Generic;
using System.Web.Mvc;

namespace BooksRental.Controllers
{
    public class GlobalModalController : Controller
    {
        [HttpPost]
        public ActionResult Moblal(string PageCalled)
        {
            ViewBag.PageCalled = PageCalled;
            return PartialView();
        }

        [HttpGet]
        public ActionResult Moblal()
        {
            return PartialView();
        }
    }
}