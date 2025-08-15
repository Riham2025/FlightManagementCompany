using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IFlightService // Represents a service for managing flight operations in the flight management system.
    {
        bool Create(string flightNumber, int routeId, int aircraftId, DateTime depUtc, DateTime arrUtc, out string error); // Creates a new flight with the specified details and validates the input.
        bool Delete(int id, out string error); // Deletes a flight by its unique identifier and validates the input.
        List<Flight> GetAll();
        List<Flight> GetByDateRange(DateTime fromUtc, DateTime toUtc);
        Flight? GetById(int id);
        bool Update(Flight flight, out string error);
    }
}