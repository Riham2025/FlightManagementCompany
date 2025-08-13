using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class BaggageRepository : IBaggageRepository// Represents a repository for managing baggage entities in the flight management system.
    {

        private readonly FlightDbContext _ctx; // Database context for accessing baggage data

        public BaggageRepository(FlightDbContext ctx)// Constructor to initialize the repository with a database context
        {
            _ctx = ctx; // Assign the provided context to the private field
        }

        public List<Baggage> GetAll() // Retrieve all baggage records from the database
        {
            return _ctx.Baggage // Represents the Baggage DbSet in the database context
                       .Include(b => b.Passenger) // Eager-load the passenger associated with the baggage
                       .AsNoTracking() // Use AsNoTracking for performance optimization
                       .ToList(); // Converts the DbSet to a list of Baggage entities.
        }

        public Baggage? GetById(int id)// Retrieve a baggage record by its unique identifier
        {
            return _ctx.Baggage // Represents the Baggage DbSet in the database context
                       .Include(b => b.Passenger) // Eager-load the passenger associated with the baggage
                       .FirstOrDefault(b => b.BaggageId == id); // Find the first baggage record that matches the specified BaggageId.
        }

        public void Add(Baggage entity) // Stage add new baggage record
        {
            _ctx.Baggage.Add(entity); // Adds a new baggage entity to the database context. This method is used to insert a new baggage record into the database.
        }

        public void Update(Baggage entity) // Stage update existing baggage record
        {
            _ctx.Baggage.Update(entity);// Updates an existing baggage entity in the database context. This method is used to modify an existing baggage record in the database.
        }

        public void Delete(int id) // Delete a baggage record by its unique identifier
        {
            var e = _ctx.Baggage.Find(id); // Find the baggage entity by its primary key (BaggageId).
            if (e != null) _ctx.Baggage.Remove(e);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
