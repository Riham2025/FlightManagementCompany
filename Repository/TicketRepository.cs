using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class TicketRepository : ITicketRepository// Represents a repository for managing ticket entities in the flight management system.
    {


        private readonly FlightDbContext _ctx; // Database context for accessing ticket data

        public TicketRepository(FlightDbContext ctx) // Constructor to initialize the repository with a database context
        {
            _ctx = ctx; // Assign the provided context to the private field
        }

        //Get all tickets with related booking and passenger
        public List<Ticket> GetAll() // Retrieve all tickets from the database
        {
            return _ctx.Tickets // Represents the Tickets DbSet in the database context
                       .Include(t => t.Booking) // Eager-load the booking associated with the ticket
                           .ThenInclude(b => b.Passenger) // Eager-load the passenger associated with the booking
                       .AsNoTracking() // Use AsNoTracking for performance optimization
                       .ToList(); 
        }

        //Find a specific ticket
        public Ticket? GetById(int id) // Retrieve a ticket by its unique identifier
        {
            return _ctx.Tickets // Represents the Tickets DbSet in the database context
                       .Include(t => t.Booking) // Eager-load the booking associated with the ticket
                           .ThenInclude(b => b.Passenger) // Eager-load the passenger associated with the booking
                       .FirstOrDefault(t => t.TicketId == id); // Find the first ticket that matches the specified TicketId.
        }

        public void Add(Ticket entity)// Stage add new ticket
        {
            _ctx.Tickets.Add(entity); // Adds a new ticket entity to the database context. This method is used to insert a new ticket record into the database.
        }

        public void Update(Ticket entity) // Stage update existing ticket
        {
            _ctx.Tickets.Update(entity); // Updates an existing ticket entity in the database context. This method is used to modify an existing ticket record in the database.
        }

        public void Delete(int id) // Delete a ticket by its unique identifier
        {
            var e = _ctx.Tickets.Find(id);
            if (e != null) _ctx.Tickets.Remove(e);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
