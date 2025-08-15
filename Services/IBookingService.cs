using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IBookingService // Interface for booking management in the flight management system.
    {
        bool Cancel(int id, out string error); // Cancels a booking by its unique identifier and validates the input.
        bool Create(int passengerId, int flightId, out string error); // Creates a new booking for a passenger on a specific flight and validates the input.
        List<Booking> GetAll(); // Retrieves all bookings from the repository, including associated passengers and flights.
        Booking? GetById(int id); // Retrieves a booking by its unique identifier, including associated passenger and flight details.
    }
}