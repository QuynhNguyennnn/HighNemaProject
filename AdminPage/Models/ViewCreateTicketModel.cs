﻿using CinemaObject;
using System.Collections.Generic;

namespace AdminPage.Models
{
    public class ViewCreateTicketModel
    {
        public Ticket Ticket { get; set; }

        public IEnumerable<Movie> Movies { get; set; }

    }
}
