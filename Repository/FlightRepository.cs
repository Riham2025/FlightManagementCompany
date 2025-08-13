using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class FlightRepository : IFlightRepository // 
    {
        

        private readonly FlightDbContext _ctx; // DbContext field

        public FlightRepository(FlightDbContext ctx) // DI constructor
        {
            _ctx = ctx; 
        }

        public List<Flight> GetAll() // Get all flights with route & aircraft
        {
            return _ctx.Flights 
                      .Include(f => f.Route) // Include route
                        .ThenInclude(r => r.Origin) //and origin airport
                      .Include(f => f.Route) 
                        .ThenInclude(r => r.Destination)// and destination airport
                      .Include(f => f.Aircraft)// Include aircraft
                      .AsNoTracking() // Read-only optimization
                      .ToList(); 
        }

        public Flight? GetById(int id)// Get a single flight with details
        {
            return _ctx.Flights 
                      .Include(f => f.Route).ThenInclude(r => r.Origin)
                      .Include(f => f.Route).ThenInclude(r => r.Destination)
                      .Include(f => f.Aircraft)
                      .FirstOrDefault(f => f.FlightId == id);
        }

        public List<Flight> GetByDateRange(DateTime fromUtc, DateTime toUtc) // Filter by departure time
        {
            return _ctx.Flights
                      .Include(f => f.Route).ThenInclude(r => r.Origin)
                      .Include(f => f.Route).ThenInclude(r => r.Destination)
                      .Include(f => f.Aircraft)
                      .Where(f => f.DepartureUtc >= fromUtc && f.DepartureUtc <= toUtc)
                      .AsNoTracking()
                      .ToList();
        }

        public void Add(Flight entity)                 // Stage insert
        {
            _ctx.Flights.Add(entity);
        }

        public void Update(Flight entity)              // Stage update
        {
            _ctx.Flights.Update(entity);
        }

        public void Delete(int id)                     // Stage delete
        {
            var e = _ctx.Flights.Find(id);            // Find by PK (tracked or from DB)
            if (e != null) _ctx.Flights.Remove(e);    // Remove if exists
        }

        public void Save() => _ctx.SaveChanges();      // Commit all staged changes
    }
}

