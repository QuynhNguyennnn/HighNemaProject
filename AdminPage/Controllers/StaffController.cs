using CinemaObject;
using DataAccess.respository;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class StaffController : Controller
    {
        IStaffRepository staffRepository = null;
        public StaffController() => staffRepository = new StaffRepository();
        // GET: MovieController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "1")
            {
                TempData["NeedLoginToBooking"] = "Please login to your account for booking ticket";
                return Redirect("/Home/Login");
            }
            var staffList = staffRepository.GetStaffs();
            return View(staffList);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var staff = staffRepository.GetStaffByID(id.Value);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    staff.Status = 1;
                    staffRepository.InsertStaff(staff);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(staff);
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var staff = staffRepository.GetStaffByID(id.Value);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, staff staff)
        {
            try
            {
                if (id != staff.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    staffRepository.UpdateStaff(staff);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var staff = staffRepository.GetStaffByID(id.Value);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                staffRepository.DeleteStaff(id);
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
