using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BooksRental.Models;
using BooksRental.Repositories;
using System.IO;
using BooksRental.Extensions;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BooksRental.POCOs;

namespace BooksRental.Controllers
{
    public class BooksController : Controller
    {
        private BookRepository repository = new BookRepository(GlobalVariables.ConnectionString);

        // GET: Books
        [Authorize()]
        public ActionResult Index()
        {
            ViewBag.Success = TempData["Success"];
            var books = repository.GetAll();
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = repository.Get(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.BookGenderId = new SelectList(repository.getAllBookGenders(), "BookGenderId", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult Create([Bind(Include = "BookId,Name,Description,BookGenderId")] Book book, HttpPostedFileBase ImageFile, string hiddenArray)
        {
            if (ModelState.IsValid)
            {
                var items = JsonConvert.DeserializeObject<List<Comment>>(hiddenArray);

                if (ImageFile != null)
                {
                    book.ImageUrl = Utils.Instance.moveMyImage(ImageFile, User.Identity.Name);
                }

                foreach (Comment com in items)
                {
                    book.Comments.Add(com);
                }

                repository.Add(book);
                repository.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BookGenderId = new SelectList(repository.getAllBookGenders(), "BookGenderId", "Name", book.BookGenderId);
            return View(book);
        }


        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = repository.Get(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookGenderId = new SelectList(repository.getAllBookGenders(), "BookGenderId", "Name", book.BookGenderId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Name,Description,ImageUrl,BookGenderId")] Book book)
        {
            if (ModelState.IsValid)
            {
                repository.Edit(book);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookGenderId = new SelectList(repository.getAllBookGenders(), "BookGenderId", "Name", book.BookGenderId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = repository.Get(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = repository.Get(id);

            string image = book.ImageUrl;

            if (!string.IsNullOrEmpty(image))
            {
                image = Path.Combine(Server.MapPath("~/Documents/BooksImages/"), image);
                System.IO.File.Delete(image);
            }

            repository.Remove(book);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
