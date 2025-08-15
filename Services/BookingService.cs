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
        private readonly PassengerRepository _passengers;
        private readonly FlightRepository _flights;

        public BookingService(BookingRepository bookings, PassengerRepository passengers, FlightRepository flights)
        {
            _bookings = bookings; _passengers = passengers; _flights = flights;
        }

        public List<Booking> GetAll() => _bookings.GetAll();
        public Booking? GetById(int id) => _bookings.GetById(id);

        public bool Create(int passengerId, int flightId, out string error)
        {
            error = string.Empty;
            if (_passengers.GetById(passengerId) == null) { error = "Passenger not found."; return false; }
            if (_flights.GetById(flightId) == null) { error = "Flight not found."; return false; }
            // prevent duplicate booking for same passenger/flight
            if (_bookings.GetAll().Any(b => b.PassengerId == passengerId && b.FlightId == flightId)) { error = "Already booked."; return false; }

            var b = new Booking { PassengerId = passengerId, FlightId = flightId };
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
