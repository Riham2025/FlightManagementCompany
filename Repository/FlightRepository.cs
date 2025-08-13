using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class FlightRepository : IFlightRepository // Represents a repository for managing flight entities in the flight management system.
    {
        

        private readonly FlightDbContext _ctx; // Database context for accessing flight data

        public FlightRepository(FlightDbContext ctx) // Constructor to initialize the repository with a database context
        {
            _ctx = ctx; // Assign the provided context to the private field
        }

        public List<Flight> GetAll() // Retrieve all flights from the database
        {
            return _ctx.Flights // Represents the Flights DbSet in the database context
                      .Include(f => f.Route) // Eager-load the route associated with the flight
                        .ThenInclude(r => r.Origin) // Eager-load the origin airport of the route
                      .Include(f => f.Route) // Eager-load the destination airport of the route
                        .ThenInclude(r => r.Destination) // Eager-load the destination airport of the route
                      .Include(f => f.Aircraft) // Eager-load the aircraft associated with the flight
                      .AsNoTracking() // Use AsNoTracking for performance optimization
                      .ToList(); // Converts the DbSet to a list of Flight entities.
        }

        public Flight? GetById(int id) // Retrieve a flight by its unique identifier
        {
            return _ctx.Flights // Represents the Flights DbSet in the database context
                      .Include(f => f.Route).ThenInclude(r => r.Origin) // Eager-load the origin airport of the route

                      .Include(f => f.Route).ThenInclude(r => r.Destination) // Eager-load the destination airport of the route
                      .Include(f => f.Aircraft) // Eager-load the aircraft associated with the flight
                      .FirstOrDefault(f => f.FlightId == id); // Find the first flight that matches the specified FlightId.
        }

        public List<Flight> GetByDateRange(DateTime fromUtc, DateTime toUtc) // Retrieve flights within a specified date range 
        {
            return _ctx.Flights // Represents the Flights DbSet in the database context
                      .Include(f => f.Route).ThenInclude(r => r.Origin) // Eager-load the origin airport of the route
                      .Include(f => f.Route).ThenInclude(r => r.Destination) // Eager-load the destination airport of the route
                      .Include(f => f.Aircraft) // Eager-load the aircraft associated with the flight
                      .Where(f => f.DepartureUtc >= fromUtc && f.DepartureUtc <= toUtc) // Filter flights based on the specified date range
                      .AsNoTracking() //Use AsNoTracking for performance optimization
                      .ToList(); 
        }

        public void Add(Flight entity) // Stage add new flight

        {
            _ctx.Flights.Add(entity); // Adds a new flight entity to the database context. This method is used to insert a new flight record into the database.
        }

        public void Update(Flight entity)  // Stage update existing flight 
        {
            _ctx.Flights.Update(entity); // Updates an existing flight entity in the database context. This method is used to modify an existing flight record in the database.
        }

        public void Delete(int id) // Delete a flight by its unique identifier 
        {
            var e = _ctx.Flights.Find(id); // Find the flight entity by its primary key (FlightId). 
            if (e != null) _ctx.Flights.Remove(e); 
        }

        public void Save() => _ctx.SaveChanges();  
    }
}

