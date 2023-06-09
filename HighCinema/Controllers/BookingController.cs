using CinemaObject;
using DataAccess;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HighCinema.Models;

namespace HighCinema.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public ActionResult Booking(int id)
        {
            if(HttpContext.Session.GetString("Role") == null)
            {
                TempData["NeedLoginToBooking"] = "Please login to your account for booking ticket";
                return Redirect("/Home/Login");
            }


            BookingModel booking = new BookingModel();

            TicketDAO ticketDAO = new TicketDAO();
            FastFoodDAO fastFoodDAO = new FastFoodDAO();
            MovieDAO movieDAO = new MovieDAO();

            booking.Movie = movieDAO.GetMovieByID(id);
            booking.Food = fastFoodDAO.GetFastFoodList();
            booking.Tickets = ticketDAO.GetTicketList();

            /*foreach(var ticket in booking.Tickets)
            {
                if (ticket.IdMovie == id)
                {
                    //tra ve ticket action create
                }
            }*/
            var listFood = new List<FastFood>();
            var listDrink = new List<FastFood>();

            foreach (var food in booking.Food)
            {
                if (food.Name == "Coca")
                {
                    listDrink.Add(food);
                }
                else
                {
                    listFood.Add(food);
                }
            }

            booking.Drink = listDrink;
            booking.Food = listFood;

            booking.Tickets = ticketDAO.GetTicketList();

            return View(booking);
        }

    }
}
