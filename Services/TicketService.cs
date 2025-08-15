using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public  class TicketService
    {

        private readonly TicketRepository _tickets; // Represents a repository for managing ticket entities in the flight management system.    
        private readonly BookingRepository _bookings; // Represents a repository for managing booking entities in the flight management system.
        private readonly FlightRepository _flights; // Represents a repository for managing flight entities in the flight management system.

        public TicketService(TicketRepository tickets, BookingRepository bookings, FlightRepository flights) // Constructor to initialize the TicketService with repositories for tickets, bookings, and flights.
        {
            _tickets = tickets; _bookings = bookings; _flights = flights; // Assign the provided repositories to the private fields
        }

        public List<Ticket> GetAll() => _tickets.GetAll();
        public Ticket? GetById(int id) => _tickets.GetById(id);

        public bool Create(int bookingId, int flightId, decimal fare, string seat, out string error)
        {
            error = string.Empty;
            if (_bookings.GetById(bookingId) == null) { error = "Booking not found."; return false; }
            if (_flights.GetById(flightId) == null) { error = "Flight not found."; return false; }
            if (fare <= 0) { error = "Fare must be positive."; return false; }

            var t = new Ticket { BookingId = bookingId, FlightId = flightId, Fare = fare, Seat = seat?.Trim() };
            _tickets.Add(t); _tickets.Save();
            return true;
        }

        public bool Update(Ticket ticket, out string error)
        {
            error = string.Empty;
            if (ticket == null || ticket.TicketId <= 0) { error = "Invalid ticket."; return false; }
            _tickets.Update(ticket); _tickets.Save();
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            _tickets.Delete(id); _tickets.Save();
            return true;
        }
    }
}
}
