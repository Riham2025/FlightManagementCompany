using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IBookingRepository // Represents an interface for managing booking entities in the flight management system.
    {
        void Add(Booking entity); // Stage add new booking
        void Delete(int id); // Delete a booking by its unique identifier
        List<Booking> GetAll();
        Booking? GetById(int id);
        void Save();
        void Update(Booking entity);
    }
}