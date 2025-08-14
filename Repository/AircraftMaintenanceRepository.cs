using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class AircraftMaintenanceRepository
    {
        private readonly FlightDbContext _ctx;// Represents the database context for accessing aircraft maintenance data

        public AircraftMaintenanceRepository(FlightDbContext ctx) // Constructor to initialize the repository with a database context
        {
            _ctx = ctx;  // Store context for later use
        }

        //Get all maintenance records with their aircraft
        public List<AircraftMaintenance> GetAll()
        {
            return _ctx.AircraftMaintenance // Represents the AircraftMaintenance DbSet in the database context
                       .Include(m => m.Aircraft) // Eager-load aircraft
                       .AsNoTracking() // Read-only optimization
                       .ToList(); // Materialize list
        }

        //Get a single maintenance record by PK
        public AircraftMaintenance? GetById(int id)
        {
            return _ctx.AircraftMaintenance // Query the DbSet
                       .Include(m => m.Aircraft)  // Include the aircraft
                       .FirstOrDefault(m => m.MaintenanceId == id); // Return first match or null
        }

        //Get all maintenance entries for a specific aircraft
        public List<AircraftMaintenance> GetByAircraft(int aircraftId)
        {
            return _ctx.AircraftMaintenance
                       .Where(m => m.AircraftId == aircraftId) //Filter by FK
                       .Include(m => m.Aircraft) // Include aircraft
                       .AsNoTracking() // Read-only
                       .ToList(); // Materialize
        }

        //Get maintenance records in the last N days
        public List<AircraftMaintenance> GetRecent(int days)
        {
            var fromDate = DateTime.UtcNow.AddDays(-Math.Abs(days)); // Compute lower bound (UTC)
            return _ctx.AircraftMaintenance
                       .Where(m => m.PerformedAtUtc >= fromDate)     // Filter by date
                       .Include(m => m.Aircraft)                      // Include aircraft
                       .AsNoTracking()                                // Read-only
                       .ToList();                                     // Materialize
        }

        // Stage insert
        public void Add(AircraftMaintenance entity)
        {
            _ctx.AircraftMaintenance.Add(entity);     // Track new entity
        }

        // Stage update
        public void Update(AircraftMaintenance entity)
        {
            _ctx.AircraftMaintenance.Update(entity);  // Mark as Modified
        }

        // Stage delete by PK
        public void Delete(int id)
        {
            var e = _ctx.AircraftMaintenance.Find(id); // Try to locate by key
            if (e != null) _ctx.AircraftMaintenance.Remove(e); // Remove if found
        }

        // Persist all staged changes
        public void Save()
        {
            _ctx.SaveChanges();                      // Commit to database
        }
    }
}
