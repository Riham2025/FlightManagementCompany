using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface ITicketRepository// Represents an interface for managing ticket entities in the flight management system.
    {
        void Add(Ticket entity);// Adds a new ticket entity to the repository.
        void Delete(int id);
        List<Ticket> GetAll();
        Ticket? GetById(int id);
        void Save();
        void Update(Ticket entity);
    }
}