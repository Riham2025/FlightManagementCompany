using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class AircraftRepository : IAircraftRepository // Represents a repository for managing aircraft entities in the flight management system.
    {

        private readonly FlightDbContext _ctx; // Database context for accessing aircraft data

        public AircraftRepository(FlightDbContext ctx) // Constructor to initialize the repository with a database context
        {
            _ctx = ctx; // Assign the provided context to the private field
        }

        // Get all aircraft
        public List<FlightManagementCompany.Models.Aircraft> GetAll()
        {
            return _ctx.Aircraft               // DbSet<Models.Aircraft>
                       .AsNoTracking()
                       .ToList();              // List<Models.Aircraft>
        }

        // Get by id
        public FlightManagementCompany.Models.Aircraft? GetById(int id)
        {
            return _ctx.Aircraft.Find(id);
        }

        // Add new aircraft
        public void Add(FlightManagementCompany.Models.Aircraft entity)
        {
            _ctx.Aircraft.Add(entity);
            _ctx.SaveChanges();
        }

        // Update existing aircraft
        public void Update(FlightManagementCompany.Models.Aircraft entity)
        {
            _ctx.Aircraft.Update(entity);
            _ctx.SaveChanges();
        }

        // Delete by id
        public void Delete(int id)
        {
            var e = _ctx.Aircraft.Find(id);
            if (e != null)
            {
                _ctx.Aircraft.Remove(e);
                _ctx.SaveChanges();
            }
        }

    }
}
