using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IBookingRepository // Represents an interface for managing booking entities in the flight management system.
    {
        void Add(Booking entity); // Stage add new booking
        void Delete(int id); // Delete a booking by its unique identifier
        List<Booking> GetAll(); // Retrieve all bookings from the database
        Booking? GetById(int id); // Retrieve a booking by its unique identifier
        void Save(); // Saves changes made to the database context
        void Update(Booking entity); // Stage update existing booking
    }
}