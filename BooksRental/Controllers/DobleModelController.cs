using BooksRental.Models;
using BooksRental.POCOs;
using BooksRental.Repositories;
using System.Collections.Generic;
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

        public ActionResult AjaxModel()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getMyAccountTypes()
        {

            var AccountTypes = AcRepo.GetAccountTypes();
            List<AccountTypePOCO> listPoco = new List<AccountTypePOCO>();
            foreach (AccountType AT in AccountTypes)
            {
                AccountTypePOCO pocoObj = new AccountTypePOCO();
                pocoObj.AccountTypeId = AT.AccountTypeId;
                pocoObj.Name = AT.Name;

                listPoco.Add(pocoObj);
            }
            return Json(listPoco, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getMyBookGenders()
        {
            var BookGenders = BGRepo.GetAll();
            List<BookGenderMetaData> listPoco = new List<BookGenderMetaData>();
            foreach (BookGender BG in BookGenders)
            {
                BookGenderMetaData pocoObj = new BookGenderMetaData();
                pocoObj.BookGenderId = BG.BookGenderId;
                pocoObj.Name = BG.Name;

                listPoco.Add(pocoObj);
            }
            return Json(listPoco, JsonRequestBehavior.AllowGet);
        }

    }
}