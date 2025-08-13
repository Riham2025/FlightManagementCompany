using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface ITicketRepository// Represents an interface for managing ticket entities in the flight management system.
    {
        void Add(Ticket entity);// Adds a new ticket entity to the repository.
        void Delete(int id); // Deletes a ticket entity by its unique identifier.
        List<Ticket> GetAll();// Retrieves all ticket entities from the repository.
        Ticket? GetById(int id);// Retrieves a ticket entity by its unique identifier.
        void Save(); // Saves changes made to the repository.
        void Update(Ticket entity); // Updates an existing ticket entity in the repository.
    }
}