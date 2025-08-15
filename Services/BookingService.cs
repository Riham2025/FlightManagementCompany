using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class BookingService
    {


        private readonly BookingRepository _bookings; // Represents a repository for managing booking entities in the flight management system.
        private readonly PassengerRepository _passengers; // Represents a repository for managing passenger entities in the flight management system.
        private readonly FlightRepository _flights; // Represents a repository for managing flight entities in the flight management system.

        public BookingService(BookingRepository bookings, PassengerRepository passengers, FlightRepository flights) // Constructor to initialize the BookingService with repositories for bookings, passengers, and flights.
        {
            _bookings = bookings; _passengers = passengers; _flights = flights; // Assign the provided repositories to the private fields.
        }

        public List<Booking> GetAll() => _bookings.GetAll(); // Retrieves all bookings from the repository, including associated passengers and flights.
        public Booking? GetById(int id) => _bookings.GetById(id); // Retrieves a booking by its unique identifier, including associated passenger and flight details.

        public bool Create(int passengerId, int flightId, out string error) // Creates a new booking for a passenger on a specific flight and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (_passengers.GetById(passengerId) == null)  // Validate that the specified passenger exists in the repository.
            { error = "Passenger not found."; return false; } 
            if (_flights.GetById(flightId) == null)  // Validate that the specified flight exists in the repository.
            { error = "Flight not found."; return false; }

            // prevent duplicate booking for same passenger/flight
            if (_bookings.GetAll().Any(b => b.PassengerId == passengerId && b.FlightId == flightId)) { error = "Already booked."; return false; } // Check if a booking for the same passenger and flight already exists in the repository.

            var b = new Booking  // Create a new Booking object with the provided passenger and flight identifiers.
            { PassengerId = passengerId, FlightId = flightId };
            _bookings.Add(b); _bookings.Save();
            return true;
        }

        public bool Cancel(int id, out string error)
        {
            error = string.Empty;
            _bookings.Delete(id); _bookings.Save();
            return true;
        }
    }
}
