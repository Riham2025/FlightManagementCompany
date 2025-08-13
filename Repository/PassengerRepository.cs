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

        //Find a passenger by their primary key (PassengerId)
        public Passenger? GetById(int id)
        {
            return _ctx.Passengers
                       .FirstOrDefault(p => p.PassengerId == id); // FirstOrDefault() is used to find the first entity that matches the specified condition, or null if no such entity exists.
        }

        //Add a new passenger to the context
        public void Add(Passenger entity)
        {
            _ctx.Passengers.Add(entity); // Adds a new passenger entity to the database context. This method is used to insert a new passenger record into the database.
        }

        //Update an existing passenger in the context
        public void Update(Passenger entity) 
        {
            _ctx.Passengers.Update(entity); // Updates an existing passenger entity in the database context. This method is used to modify an existing passenger record in the database.
        }

        //Remove a passenger by id
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
