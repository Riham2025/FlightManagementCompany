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
                       .AsNoTracking()
                       .ToList();
        }

        // Find a specific ticket
        public Ticket? GetById(int id)
        {
            return _ctx.Tickets
                       .Include(t => t.Booking)
                           .ThenInclude(b => b.Passenger)
                       .FirstOrDefault(t => t.TicketId == id);
        }

        public void Add(Ticket entity)
        {
            _ctx.Tickets.Add(entity);
        }

        public void Update(Ticket entity)
        {
            _ctx.Tickets.Update(entity);
        }

        public void Delete(int id)
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
