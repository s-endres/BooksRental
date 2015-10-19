using BooksRental.Models;
using BooksRental.POCOs;
using BooksRental.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksRental.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ShoppingCartRepository repository = new ShoppingCartRepository(GlobalVariables.ConnectionString);

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cartBook = repository.GetAll();
            return View(cartBook);
        }
        public ActionResult addBookToCart(int? id)
        {
            if (id.HasValue) {
                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.AccountId = int.Parse(User.Identity.Name);
                shoppingCart.BookId = id.Value;
                shoppingCart.RentedDate = DateTime.Now;

                repository.Add(shoppingCart);
                repository.SaveChanges();

            }

            TempData["Success"] = "The book has been rented";
            return RedirectToAction("Index", "Books");
        }
        public ActionResult removeBookFromCart(int id)
        {
            ShoppingCart objSC = repository.Get(id);
            repository.Remove(objSC);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}