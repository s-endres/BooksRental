﻿using BooksRental.Models;
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
        [Authorize(Roles = "Admin,Renter")]
        public ActionResult Index()
        {
            var cartBook = repository.getAllByAccountId(int.Parse(User.Identity.Name));
            return View(cartBook);
        }
        [Authorize(Roles = "Admin,Renter")]
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
        [Authorize(Roles = "Admin,Renter")]
        public ActionResult removeBookFromCart(int id)
        {
            ShoppingCart objSC = repository.Get(id);
            repository.Remove(objSC);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}