using CinemaObject;
using DataAccess.respository;
using DataAccess;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class BillController : Controller
    {
        IBillRepository billRepository = null;
        public BillController() => billRepository = new BillRepository();
        // GET: FastFoodController
        public ActionResult Index()
        {
            var billList = billRepository.GetBills();
            return View(billList);
        }

        // GET: FastFoodController/Details/5
        // POST: FastFoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bill bill)
        {

            Console.WriteLine(bill.TicketId + " " + bill.IdMovie + " " + bill.FastFood + " " + bill.SeatNum + " " + bill.QuantityDrink + " " + bill.QuantityFastfood + " " + bill.TicketType + " " + bill.Drink);

            try
            {
                TicketDAO ticketDAO = new TicketDAO();
                FastFoodDAO fastFoodDAO = new FastFoodDAO();
                BillDAO billDAO = new BillDAO();
                Bill _bill = billDAO.GetBillByID(bill.Id);

                

                if (_bill == null)
                {
                    FastFood Food = fastFoodDAO.GetFastFoodByID(bill.FastFood);
                    FastFood Drink = fastFoodDAO.GetFastFoodByID(bill.Drink);
                    Ticket ticket = ticketDAO.GetTicketByID(bill.TicketId);

                    bill.TicketType = ticket.TicketType;
                    bill.Total = 0;

                    bill.BookingDate = DateTime.Today;
                    bill.Total = ticket.Price + Food.Price * bill.QuantityFastfood + Drink.Price * bill.QuantityDrink;
                    Console.WriteLine(bill.TicketId + " " + bill.IdMovie + " " + bill.FastFood + " " + bill.SeatNum + " " + bill.QuantityDrink + " " + bill.QuantityFastfood + " " + bill.TicketType + " " + bill.Total + " " + bill.Drink);
                    using var context = new CinemaProject_v4Context();
                    context.Bills.Add(bill);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The fast food is already exist.");
                }
                if (_bill != null)
                {
                    billRepository.InsertBill(bill);
                }
                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: FastFoodController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bill = billRepository.GetBillByID(id.Value);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // POST: FastFoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Bill bill)
        {
            try
            {
                if (id != bill.Id)
                    return NotFound();
                if (ModelState.IsValid)
                    billRepository.UpdateBill(bill);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: FastFoodController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bill = billRepository.GetBillByID(id.Value);
            if (bill == null)
                return NotFound();
            return View(bill);
        }

        // POST: FastFoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                billRepository.DeleteBill(id);
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
