using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class FlightService
    {



        private readonly FlightRepository _flights; // Repository for accessing flight data
        private readonly RouteRepository _routes; // Repository for accessing route data
        private readonly AircraftRepository _aircraft; // Repository for accessing aircraft data

        public FlightService(FlightRepository flights, RouteRepository routes, AircraftRepository aircraft) // Constructor to initialize the service with repositories
        {
            _flights = flights; _routes = routes; _aircraft = aircraft; // Assign the provided repositories to the private fields
        }

        public List<Flight> GetAll() => _flights.GetAll(); // Retrieves all flights from the repository, including associated routes and aircraft.
        public Flight? GetById(int id) => _flights.GetById(id); // Retrieves a flight by its unique identifier, including associated route and aircraft details.
        public List<Flight> GetByDateRange(DateTime fromUtc, DateTime toUtc) => _flights.GetByDateRange(fromUtc, toUtc); // Retrieves flights within a specified date range, including associated routes and aircraft details.

        public bool Create(string flightNumber, int routeId, int aircraftId, DateTime depUtc, DateTime arrUtc, out string error) // Creates a new flight with the specified details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (string.IsNullOrWhiteSpace(flightNumber)) { error = "Flight number required."; return false; } // Validate flight number.
            if (arrUtc <= depUtc) { error = "Arrival must be after departure."; return false; } // Validate that the arrival time is after the departure time.
            if (_routes.GetById(routeId) == null) { error = "Route not found."; return false; } // Validate that the specified route exists in the repository.
            if (_aircraft.GetById(aircraftId) == null) { error = "Aircraft not found."; return false; }
            // basic uniqueness check on same departure timestamp
            if (_flights.GetAll().Any(f => f.FlightNumber == flightNumber.Trim() && f.DepartureUtc == depUtc)) { error = "Duplicate flight/departure."; return false; }

            var f = new Flight { FlightNumber = flightNumber.Trim().ToUpper(), RouteId = routeId, AircraftId = aircraftId, DepartureUtc = depUtc, ArrivalUtc = arrUtc };
            _flights.Add(f); _flights.Save();
            return true;
        }

        public bool Update(Flight flight, out string error)
        {
            error = string.Empty;
            if (flight == null || flight.FlightId <= 0) { error = "Invalid flight."; return false; }
            if (flight.ArrivalUtc <= flight.DepartureUtc) { error = "Arrival must be after departure."; return false; }
            _flights.Update(flight); _flights.Save();
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            _flights.Delete(id); _flights.Save();
            return true;
        }
    }
}
