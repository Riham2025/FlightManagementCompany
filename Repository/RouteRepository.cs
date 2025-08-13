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
                      .Include(r => r.Destination)     // Eager-load destination airport
                      .AsNoTracking()                  // Read-only optimization
                      .ToList();                       // Materialize result
        }

        public Route? GetById(int id)                  // Retrieve a single route by key
        {
            return _ctx.Routes
                      .Include(r => r.Origin)
                      .Include(r => r.Destination)
                      .FirstOrDefault(r => r.RouteId == id);
        }

        public void Add(Route entity)                  // Stage insert
        {
            _ctx.Routes.Add(entity);
        }

        public void Update(Route entity)               // Stage update
        {
            _ctx.Routes.Update(entity);
        }

        public void Delete(int id)                     // Stage delete by key
        {
            var e = _ctx.Routes.Find(id);             // Try to find tracked/attached entity
            if (e != null) _ctx.Routes.Remove(e);     // Remove if found
        }

        public void Save() => _ctx.SaveChanges();
    }
}
