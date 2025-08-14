using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class FlightCrewRepository : IFlightCrewRepository // Represents a repository for managing flight crew assignments in the flight management system.
    {


        private readonly FlightDbContext _ctx; // Context used for persistence

        public FlightCrewRepository(FlightDbContext ctx)
        {
            _ctx = ctx; // Save context reference
        }

        //Return all assignments, including linked Flight and CrewMember
        public List<FlightCrew> GetAll()
        {
            return _ctx.FlightCrew
                       .Include(fc => fc.Flight) // Eager-load the Flight
                       .Include(fc => fc.CrewMember) // Eager-load the CrewMember
                       .AsNoTracking() // Read-only optimization
                       .ToList(); // Materialize
        }

        //Check if an assignment exists using composite key
        public bool Exists(int flightId, int crewId)
        {
            return _ctx.FlightCrew
                       .Any(fc => fc.FlightId == flightId && fc.CrewId == crewId); // Check existence by composite key
        }

        //Create a new assignment
        public void Add(FlightCrew entity)
        {
            _ctx.FlightCrew.Add(entity); // Stage insert
        }

        //Remove an assignment by composite key
        public void Delete(int flightId, int crewId)
        {
            //Find accepts a key array for composite keys
            var e = _ctx.FlightCrew.Find(flightId, crewId);
            if (e != null) _ctx.FlightCrew.Remove(e); //Remove if found
        }

        //Retrieve all crew members assigned to a specific flight
        public List<CrewMember> GetCrewForFlight(int flightId)
        {
            return _ctx.FlightCrew
                       .Where(fc => fc.FlightId == flightId) // Filter by flight
                       .Include(fc => fc.CrewMember) // Load crew
                       .Select(fc => fc.CrewMember!)             // Project to CrewMember
                       .AsNoTracking()                            // Read-only
                       .ToList();                                 // Materialize
        }

        // Retrieve all flights assigned to a specific crew member
        public List<Flight> GetFlightsForCrew(int crewId)
        {
            return _ctx.FlightCrew
                       .Where(fc => fc.CrewId == crewId)         // Filter by crew
                       .Include(fc => fc.Flight)                  // Load flight
                           .ThenInclude(f => f.Route)             // Load Route
                       .Include(fc => fc.Flight)                  // Load again to chain
                           .ThenInclude(f => f.Aircraft)          // Load Aircraft
                       .Select(fc => fc.Flight!)                  // Project to Flight
                       .AsNoTracking()                            // Read-only
                       .ToList();                                 // Materialize
        }

        // Persist changes
        public void Save()
        {
            _ctx.SaveChanges();                      // Commit unit of work
        }
    }
}


