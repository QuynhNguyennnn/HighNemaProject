using System;
using CinemaObject;
using DataAccess.respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository accountRespository = null;
        public AccountController() => accountRespository = new AccountRepository();

        // GET: AccountController
        public ActionResult Index()
        {
            var accountList = accountRespository.GetAccounts();
            return View(accountList);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var account = accountRespository.GetAccountByID(id.Value);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountRespository.InsertAccount(account);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.InnerException.Message;
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = accountRespository.GetAccountByID(id.Value);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Account account)
        {
            try
            {
                if (id != account.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    accountRespository.UpdateAccount(account);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = accountRespository.GetAccountByID(id.Value);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                accountRespository.DeleteAccount(id);
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
