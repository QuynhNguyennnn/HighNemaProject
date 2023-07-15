using CinemaObject;
using DataAccess.respository;
using DataAccess;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminPage.Models;

namespace AdminPage.Controllers
{
    public class TicketController : Controller
    {
        ITicketRepository ticketRepository = null;
        public TicketController() => ticketRepository = new TicketRepository();
        // GET: FastFoodController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "1")
            {
                TempData["NeedLoginToBooking"] = "Please login to your account for booking ticket";
                return Redirect("/Home/Login");
            }
            var ticketList = ticketRepository.GetTickets();
            return View(ticketList);
        }

        // GET: FastFoodController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = ticketRepository.GetTicketByID(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: FastFoodController/Create
        [HttpGet]
        public ActionResult Create()
        {
            MovieDAO movieDAO = new MovieDAO();
            ViewCreateTicketModel viewCreateTicketModel = new ViewCreateTicketModel();
            viewCreateTicketModel.Movies = movieDAO.GetMovieList();
            return View(viewCreateTicketModel);
        }

        // POST: FastFoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ticketRepository.InsertTicket(ticket);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(ticket);
            }
        }

        // GET: FastFoodController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = ticketRepository.GetTicketByID(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: FastFoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Ticket ticket)
        {
            try
            {
                if (id != ticket.Id)
                    return NotFound();
                if (ModelState.IsValid)
                    ticketRepository.UpdateTicket(ticket);
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
            var ticket = ticketRepository.GetTicketByID(id.Value);
            if (ticket == null)
                return NotFound();
            return View(ticket);
        }

        // POST: FastFoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                ticketRepository.DeleteTicket(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Order()
        {
            var ticketList = ticketRepository.GetTickets();
            return View(ticketList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(int? id)
        {
            string txt = "";
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            if (Request.Cookies["ticket"] == null)
            {
                txt = id + ":1";
                Response.Cookies.Append("ticket", txt, options);
            }
            else
            {
                TicketDAO ticketDAO = new TicketDAO();
                txt = Convert.ToString(Request.Cookies["ticket"]);
                Response.Cookies.Append("ticket", "", options);
                string[] t = txt.Split(";");
                var ticketList = new List<Ticket>();
                for (int i = 0; i < t.Length; i++)
                {
                    string[] a = t[i].Split(":");
                    Ticket ticket = new Ticket();
                    ticket = ticketDAO.GetTicketByID(Convert.ToInt32(a[0]));
                    ticket.Quantity = Convert.ToInt32(a[1]);
                    ticketList.Add(ticket);
                }

                int check = 0;

                foreach (Ticket ticket in ticketList)
                {
                    if (ticket.Id == id)
                    {
                        ticket.Quantity++;
                        check = 1;
                        break;
                    }
                }

                txt = ticketList[0].Id + ":" + ticketList[0].Quantity;

                for (int i = 1; i < ticketList.Count; i++)
                {
                    txt += ";" + ticketList[i].Id + ":" + ticketList[i].Quantity;
                }
                if (check == 0)
                {
                    txt += ";" + id + ":1";
                }

                Response.Cookies.Append("ticket", txt, options);
            }
            var tickets = ticketRepository.GetTickets();
            return View(tickets);
        }
    }
}
