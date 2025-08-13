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
        public List<FlightManagementCompany.Models.Aircraft> GetAll() // Retrieves all aircraft from the database.
        {
            return _ctx.Aircraft  // Represents the Aircraft DbSet in the database context             
                       .AsNoTracking() // AsNoTracking() is used to improve performance by not tracking changes to the entities.
                       .ToList();// Converts the DbSet to a list of Aircraft entities.         
        }

        // Get by id
        public FlightManagementCompany.Models.Aircraft? GetById(int id) // Retrieves an aircraft by its unique identifier.
        {
            return _ctx.Aircraft.Find(id); // Find() is used to retrieve an entity by its primary key, which is the AircraftId in this case.
        }

        // Add new aircraft
        public void Add(FlightManagementCompany.Models.Aircraft entity) // Adds a new aircraft entity to the database context.
        {
            _ctx.Aircraft.Add(entity); // This method is used to insert a new aircraft record into the database.
            _ctx.SaveChanges(); // Saves changes made to the database context, persisting the new aircraft entity.
        }

        // Update existing aircraft
        public void Update(FlightManagementCompany.Models.Aircraft entity) // Updates an existing aircraft entity in the database context.
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
