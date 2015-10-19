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
using System.Web.Security;
using BooksRental.Extensions;
using BooksRental.POCOs;

namespace BooksRental.Controllers
{
    public class AccountsController : Controller
    {
        private AccountRepository repository = new AccountRepository();

        /// <summary>
        /// GET: Accounts
        /// </summary>
        /// <returns>All accounts register in the system</returns>
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        // 
        /// <summary>
        /// GET: Details of a selected account
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The account object</returns>
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = repository.Get(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        /// <summary>
        /// GET: Create account form
        /// </summary>
        /// <returns>View</returns>
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            ViewBag.AccountTypeId = new SelectList(repository.GetAccountTypes(), "AccountTypeId", "Name");
            return View();
        }

        /// <summary>
        /// POST: Accounts/Create
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="account">Object account</param>
        /// <returns>View with account object</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create([Bind(Include = "AccountId,Email,Password,Name,AccountTypeId")] Account account)
        {
            ViewBag.AccountTypeId = new SelectList(repository.GetAccountTypes(), "AccountTypeId", "Name", account.AccountTypeId);

            if (ModelState.IsValid)
            {
                List<Account> accounts = repository.GetByEmail(account.Email);
                if (accounts.Count == 0)
                {
                    account.Password = Utils.Instance.Encript(account.Password);
                    repository.Add(account);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "This e-mail is already taken.";
                }
            }
            return View(account);
        }

        /// <summary>
        /// GET: Edition of an existing account
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>View with account obkect</returns>
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = repository.Get(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountTypeId = new SelectList(repository.GetAccountTypes(), "AccountTypeId", "Name", account.AccountTypeId);
            return View(account);
        }

        /// <summary>
        /// POST: Accounts/Edit/5
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns>Success:Index View, Fail:Error 500 View, Invalid Form: View Account object</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit([Bind(Include = "AccountId,Email,Password,Name,AccountTypeId")] Account account)
        {
            if (ModelState.IsValid)
            {
                List<Account> accounts = repository.GetByEmail(account.Email);
                if (accounts.Count == 0 || account.AccountId == accounts[0].AccountId)
                {
                    repository = new AccountRepository();
                    repository.Edit(account);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "This e-mail is already taken.";
                }
            }
            ViewBag.AccountTypeId = new SelectList(repository.GetAccountTypes(), "AccountTypeId", "Name", account.AccountTypeId);
            return View(account);
        }

        /// <summary>
        /// GET: Accounts/Delete/5
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>View account object</returns>
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = repository.Get(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        /// <summary>
        /// POST: Accounts/Delete/5
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>Index view </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = repository.Get(id);
            repository.Remove(account);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: Login
        /// </summary>
        /// <returns>View</returns>
        public ActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        /// POST: Login
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns>Success: Dashboard View, Error: View</returns>
        [HttpPost]
        public ActionResult LogIn([Bind(Include = "Email,Password")]Account account)
        {
            Account objAccount = isValid(account.Email, account.Password);
            if (objAccount != null)
            {
                FormsAuthentication.SetAuthCookie(objAccount.Email, false);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                      1,
                      objAccount.AccountId.ToString(),  //user email
                      DateTime.Now,
                      DateTime.Now.AddDays(1),  // expiry
                      false,  //do not remember
                      objAccount.AccountType.Name,
                      "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                   FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Email or Password are incorrect, please try again.";
                return View(account);
            }
        }

        /// <summary>
        /// POST: Recover Password
        /// </summary>
        /// <param name="Email">E-mail address</param>
        /// <returns>Send status:Fail, Success, Error</returns>
        [HttpPost]
        public ActionResult RecoverPassword(string Email)
        {
            Random rnd = new Random();
            List<Account> accounts = repository.GetByEmail(Email);
            string wasSend;

            if (accounts.Count > 0)
            {
                int intPass = rnd.Next(99999);
                try
                {
                    accounts[0].Password = Utils.Instance.Encript(intPass.ToString());
                    repository.Edit(accounts[0]);
                    repository.SaveChanges();

                    string content = "Your new password is: " + intPass.ToString();
                    wasSend = Utils.Instance.SendEmail(Email, "Password Recovery", content).ToString();
                }
                catch
                {
                    wasSend = "Error";
                }
            }
            else
            {
                wasSend = "True";
            }

            return Json(wasSend);
        }

        /// <summary>
        /// GET: LogOut
        /// </summary>
        /// <returns>Index Home View</returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">is being dispose?</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Validates the user login
        /// </summary>
        /// <param name="pEmail">e-mail</param>
        /// <param name="pPassword">password</param>
        /// <returns>Valid State</returns>
        protected Account isValid(string pEmail, string pPassword)
        {
            bool result = false;
            List<Account> accounts = repository.GetByEmail(pEmail);
            if (accounts.Count > 0)
            {
                if (accounts[0].Password == Utils.Instance.Encript(pPassword))
                {
                    return accounts[0];
                }
            }

            return null;
        }
    }
}
