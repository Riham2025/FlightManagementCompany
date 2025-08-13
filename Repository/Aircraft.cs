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
            _ctx.Aircraft.AsNoTracking().ToList();

        public Aircraft? GetById(int id) =>
            _ctx.Aircraft.Find(id);

        public Aircraft? GetByTail(string tailNumber) =>
            _ctx.Aircraft.AsNoTracking().FirstOrDefault(a => a.TailNumber == tailNumber);

        public void Add(Aircraft entity) => _ctx.Aircraft.Add(entity);
        public void Update(Aircraft entity) => _ctx.Aircraft.Update(entity);

        public void Delete(int id)
        {
            var e = _ctx.Aircraft.Find(id);
            if (e != null) _ctx.Aircraft.Remove(e);
        }

        public void Save() => _ctx.SaveChanges();
    }
}
