using BooksRental.POCOs;
using BooksRental.Repositories;
using System.Web.Mvc;

namespace BooksRental.Controllers
{
    public class DobleModelController : Controller
    {
        private AccountRepository AcRepo = new AccountRepository(GlobalVariables.ConnectionString);
        private BookGenderRepository BGRepo = new BookGenderRepository(GlobalVariables.ConnectionString);

        // GET: DobleModel
        public ActionResult Index()
        {
            ViewData["AccountTypes"] = AcRepo.GetAccountTypes();
            ViewData["BookGenders"] = BGRepo.GetAll();

            return View();
        }
    }
}