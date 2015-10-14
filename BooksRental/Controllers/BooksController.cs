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

namespace BooksRental.Controllers
{
    public class BooksController : Controller
    {
        private BookRepository repository = new BookRepository();

        // GET: Books
        public ActionResult Index()
        {
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
        public ActionResult Create([Bind(Include = "BookId,Name,Description,ImageUrl,BookGenderId")] Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    book.ImageUrl = moveMyImage(ImageFile);
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

        protected string moveMyImage(HttpPostedFileBase ImageFile)
        {
            string[] name = ImageFile.FileName.Split('.');
            var userId = User.Identity.Name;
            var fileName = userId.ToString() + DateTime.Now.ToString("MMddyyyyhhmmss") + "." + name[1];
            var filePath = Path.Combine(Server.MapPath("~/Documents/BooksImages/"), fileName);
            ImageFile.SaveAs(filePath);
            var imageName = fileName.ToString();
            return imageName.ToString();
        }
    }
}
