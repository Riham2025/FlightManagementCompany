using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class PassengerRepository
    {

        private readonly FlightDbContext _ctx;// EF Core DbContext


        //Constructor injection to ensure DbContext is provided
        public PassengerRepository(FlightDbContext ctx) // Represents a repository for managing passenger entities in the flight management system.
        {
            _ctx = ctx;
        }

        //Get all passengers with no tracking for better performance
        public List<Passenger> GetAll()
        {
            return _ctx.Passengers // Represents the Passengers DbSet in the database context
                       .AsNoTracking() // AsNoTracking() is used to improve performance by not tracking changes to the entities.
                       .ToList();
        }

        // Find a passenger by their primary key (PassengerId)
        public Passenger? GetById(int id)
        {
            return _ctx.Passengers
                       .FirstOrDefault(p => p.PassengerId == id);
        }

        // Add a new passenger to the context
        public void Add(Passenger entity)
        {
            _ctx.Passengers.Add(entity);
        }

        // Update an existing passenger in the context
        public void Update(Passenger entity)
        {
            _ctx.Passengers.Update(entity);
        }

        // Remove a passenger by id
        public void Delete(int id)
        {
            var e = _ctx.Passengers.Find(id);
            if (e != null) _ctx.Passengers.Remove(e);
        }

        // Commit staged changes to the database
        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
