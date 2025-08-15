using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class TicketService : ITicketService // Service class for managing ticket operations in the flight management system.
    {

        private readonly TicketRepository _tickets; // Represents a repository for managing ticket entities in the flight management system.    
        private readonly BookingRepository _bookings; // Represents a repository for managing booking entities in the flight management system.
        private readonly FlightRepository _flights; // Represents a repository for managing flight entities in the flight management system.

        public TicketService(TicketRepository tickets, BookingRepository bookings, FlightRepository flights) // Constructor to initialize the TicketService with repositories for tickets, bookings, and flights.
        {
            _tickets = tickets; _bookings = bookings; _flights = flights; // Assign the provided repositories to the private fields
        }

        public List<Ticket> GetAll() => _tickets.GetAll(); // Retrieves all tickets from the repository, including associated bookings and passengers.
        public Ticket? GetById(int id) => _tickets.GetById(id); // Retrieves a ticket by its unique identifier, including associated booking and passenger details.

        public bool Create(int bookingId, int flightId, decimal fare, string seat, out string error) // Creates a new ticket for a booking on a specific flight and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (_bookings.GetById(bookingId) == null) // Validate that the specified booking exists in the repository.
            { error = "Booking not found."; return false; }

            if (_flights.GetById(flightId) == null)   // Validate that the specified flight exists in the repository.  
            { error = "Flight not found."; return false; }
            if (fare <= 0) { error = "Fare must be positive."; return false; } // Validate that the fare is a positive value.

            var t = new Ticket // Create a new Ticket object with the provided booking ID, flight ID, fare, and seat information.
            { BookingId = bookingId, FlightId = flightId, Fare = fare, Seat = seat?.Trim() };
            _tickets.Add(t); // Stage the new ticket for addition to the repository.
            _tickets.Save(); // Save the changes to the repository after adding the new ticket.
            return true;
        }

        public bool Update(Ticket ticket, out string error) // Updates an existing ticket with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (ticket == null || ticket.TicketId <= 0) // Validate the ticket object and its ID. 
            { error = "Invalid ticket."; return false; } // Validate that the ticket object is not null and has a valid ID.
            _tickets.Update(ticket); // Stage the updated ticket for modification in the repository.
            _tickets.Save(); // Save the updated ticket to the repository.
            return true;
        }

        public bool Delete(int id, out string error) // Deletes a ticket by its unique identifier and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            _tickets.Delete(id); _tickets.Save(); // Save the changes to the repository after deletion.
            return true;
        }
    }
}

