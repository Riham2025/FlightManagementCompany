using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class Aircraft 
    {

        private readonly FlightDbContext _ctx; // Represents a repository for managing aircraft entities in the flight management system.
        public Aircraft(FlightDbContext ctx) => _ctx = ctx; // Constructor to initialize the repository with a database context

        public List<Aircraft> GetAll() => // Retrieves all aircraft from the database.
            _ctx.Aircraft.AsNoTracking().ToList();// AsNoTracking() is used to improve performance by not tracking changes to the entities.

        public Aircraft? GetById(int id) => // Retrieves an aircraft by its unique identifier.
            _ctx.Aircraft.Find(id); // Find() is used to retrieve an entity by its primary key, which is the AircraftId in this case.

        public Aircraft? GetByTail(string tailNumber) => // Retrieves an aircraft by its tail number.
            _ctx.Aircraft.AsNoTracking().FirstOrDefault(a => a.TailNumber == tailNumber); // FirstOrDefault() is used to find the first entity that matches the specified condition, or null if no such entity exists.

        public void Add(Aircraft entity) => _ctx.Aircraft.Add(entity); // Adds a new aircraft entity to the database context. This method is used to insert a new aircraft record into the database.
        public void Update(Aircraft entity) => _ctx.Aircraft.Update(entity); // Updates an existing aircraft entity in the database context. This method is used to modify an existing aircraft record in the database.

        public void Delete(int id) // Deletes an aircraft by its unique identifier.
        {
            var e = _ctx.Aircraft.Find(id); // Find the aircraft entity by its primary key (AircraftId).
            if (e != null) _ctx.Aircraft.Remove(e); // If the entity is found, remove it from the database context. This method is used to delete an aircraft record from the database.
        }

        public void Save() => _ctx.SaveChanges();
    }
}
