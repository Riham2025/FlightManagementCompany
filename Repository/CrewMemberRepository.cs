using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class CrewMemberRepository : ICrewMemberRepository // Represents a repository for managing crew member entities in the flight management system.
    {

        //Private context reference used to access the database
        private readonly FlightDbContext _ctx;

        //Constructor injection of DbContext
        public CrewMemberRepository(FlightDbContext ctx)
        {
            _ctx = ctx; // Store context for later use
        }

        //Retrieve all crew members (read-only)
        public List<CrewMember> GetAll()
        {
            return _ctx.CrewMembers // Start from CrewMembers table
                       .AsNoTracking() // No tracking for speed on reads
                       .ToList(); // Materialize as List<CrewMember>
        }

        //Find a single crew member by primary key
        public CrewMember? GetById(int id)
        {
            return _ctx.CrewMembers // Query the DbSet
                       .FirstOrDefault(c => c.CrewId == id);// Return first match or null
        }

        //Insert a new crew member
        public void Add(CrewMember entity)
        {
            _ctx.CrewMembers.Add(entity); // Stage insert in change tracker
        }

        //Update an existing crew member
        public void Update(CrewMember entity)
        {
            _ctx.CrewMembers.Update(entity);// Mark as Modified
        }

        //Delete a crew member by key
        public void Delete(int id)
        {
            var e = _ctx.CrewMembers.Find(id); // Attempt to locate by PK
            if (e != null) _ctx.CrewMembers.Remove(e); // Remove if found
        }

        // Persist all staged changes
        public void Save()
        {
            _ctx.SaveChanges();                      // Flush to database
        }
    }
}
