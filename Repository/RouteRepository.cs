using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class RouteRepository : IRouteRepository // Represents a repository for managing flight routes in the flight management system.
    {

        private readonly FlightDbContext _ctx; // Database context for accessing route data

        public RouteRepository(FlightDbContext ctx) // Constructor to initialize the repository with a database context
        {
            _ctx = ctx; // Assign the provided context to the private field
        }

        public List<Route> GetAll() // Retrieve all routes from the database                 
        {
            return _ctx.Routes // Represents the Routes DbSet in the database context
                      .Include(r => r.Origin) // Eager-load origin airport
                      .Include(r => r.Destination)   // Eager-load destination airport
                      .AsNoTracking()   // Use AsNoTracking for performance optimization
                      .ToList();  // Converts the DbSet to a list of Route entities.
        }

        public Route? GetById(int id) // Retrieve a route by its unique identifier
        {
            return _ctx.Routes // Represents the Routes DbSet in the database context
                      .Include(r => r.Origin) // Eager-load origin airport
                      .Include(r => r.Destination) // Eager-load destination airport
                      .FirstOrDefault(r => r.RouteId == id); // Find the first route that matches the specified RouteId.
        }

        public void Add(Route entity) // Stage add new route
        {
            _ctx.Routes.Add(entity); // Adds a new route entity to the database context.
        }

        public void Update(Route entity) // Stage update existing route
        {
            _ctx.Routes.Update(entity); // Updates an existing route entity in the database context.
        }

        public void Delete(int id)  // Delete a route by its unique identifier
        {
            var e = _ctx.Routes.Find(id); // Find the route entity by its primary key (RouteId).
            if (e != null) _ctx.Routes.Remove(e);     // Remove if found
        }

        public void Save() => _ctx.SaveChanges();
    }
}
