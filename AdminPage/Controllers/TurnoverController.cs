using CinemaObject;
using DataAccess;
using AdminPage.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class TurnoverController : Controller
    {
        public ActionResult Index()
        {
            TicketDAO ticketDAO = new TicketDAO();
            FastFoodDAO fastFoodDAO = new FastFoodDAO();
            BillDAO billDAO = new BillDAO();

            var billList = billDAO.GetBills();

            TurnoverModel turnover = new TurnoverModel();

            DateTime today = DateTime.Today;

            int month = today.Month;
            int year = today.Year;

            turnover.month = month;
            turnover.year = year;

            turnover.mfTotal = 0;
            turnover.mtTotal = 0;
            turnover.yfTotal = 0;

            foreach (var bill in billList)
            {
                if (bill.BookingDate.Month == month && bill.BookingDate.Year == year)
                {
                    FastFood food = fastFoodDAO.GetFastFoodByID(bill.FastFood);
                    FastFood drink = fastFoodDAO.GetFastFoodByID(bill.Drink);
                    Ticket ticket = ticketDAO.GetTicketByID(bill.TicketId);

                    turnover.mfTotal += food.Price * bill.QuantityFastfood + drink.Price * bill.QuantityDrink;
                    turnover.mtTotal += ticket.Price;
                }
                if (bill.BookingDate.Year == year)
                {
                    FastFood food = fastFoodDAO.GetFastFoodByID(bill.FastFood);
                    FastFood drink = fastFoodDAO.GetFastFoodByID(bill.Drink);
                    Ticket ticket = ticketDAO.GetTicketByID(bill.TicketId);

                    turnover.yfTotal += food.Price * bill.QuantityFastfood + drink.Price * bill.QuantityDrink;
                    turnover.ytTotal += ticket.Price;
                }
            }

            return View(turnover);
        }

        // GET: TurnoverController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
