using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IBookingService // Interface for booking management in the flight management system.
    {
        bool Cancel(int id, out string error); // Cancels a booking by its unique identifier and validates the input.
        bool Create(int passengerId, int flightId, out string error);
        List<Booking> GetAll();
        Booking? GetById(int id);
    }
}