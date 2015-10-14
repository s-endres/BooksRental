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

namespace BooksRental.Controllers
{
    public class BookGendersController : Controller
    {
        private BookGenderRepository repository = new BookGenderRepository();

        // GET: BookGenders
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        // GET: BookGenders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookGender bookGender = repository.Get(id);
            if (bookGender == null)
            {
                return HttpNotFound();
            }
            return View(bookGender);
        }

        // GET: BookGenders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookGenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookGenderId,Name")] BookGender bookGender)
        {
            if (ModelState.IsValid)
            {
                repository.Add(bookGender);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookGender);
        }

        // GET: BookGenders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookGender bookGender = repository.Get(id);
            if (bookGender == null)
            {
                return HttpNotFound();
            }
            return View(bookGender);
        }

        // POST: BookGenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookGenderId,Name")] BookGender bookGender)
        {
            if (ModelState.IsValid)
            {
                repository.Edit(bookGender);
                return RedirectToAction("Index");
            }
            return View(bookGender);
        }

        // GET: BookGenders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookGender bookGender = repository.Get(id);
            if (bookGender == null)
            {
                return HttpNotFound();
            }
            return View(bookGender);
        }

        // POST: BookGenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookGender bookGender = repository.Get(id);
            repository.Remove(bookGender);
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
