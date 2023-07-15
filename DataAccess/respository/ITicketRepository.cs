using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetTickets();
        IEnumerable<Ticket> GetTicketsByMovieID(int movieID);
        Ticket GetTicketByID(int ticketId);
        void DeleteTicket(int ticketId);
        void InsertTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
    }
}
