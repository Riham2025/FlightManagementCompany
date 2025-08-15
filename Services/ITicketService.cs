using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface ITicketService // Represents a service for managing ticket operations in the flight management system.
    {
        bool Create(int bookingId, int flightId, decimal fare, string seat, out string error);
        bool Delete(int id, out string error);
        List<Ticket> GetAll();
        Ticket? GetById(int id);
        bool Update(Ticket ticket, out string error);
    }
}