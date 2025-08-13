using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class Airports : IAirports 
    {
        // Represents a repository for managing airports in the flight management system.
        private readonly FlightDbContext _ctx; // Database context for accessing airport data
        public Airports(FlightDbContext ctx) => _ctx = ctx; // Constructor to initialize the repository with a database context



        public List<Airport> GetAll() => // Retrieves all airports from the database.
            _ctx.Airports.AsNoTracking().ToList();// AsNoTracking() is used to improve performance by not tracking changes to the entities.


        public Airport? GetById(int id) => // Retrieves an airport by its unique identifier.
            _ctx.Airports.Find(id);// Find() is used to retrieve an entity by its primary key, which is the AirportId in this case.

        public Airport? GetByIata(string iata) => // Retrieves an airport by its IATA code.
            _ctx.Airports.AsNoTracking().FirstOrDefault(a => a.IATA == iata); // FirstOrDefault() is used to find the first entity that matches the specified condition, or null if no such entity exists.

        public void Add(Airport entity) => _ctx.Airports.Add(entity); // Adds a new airport entity to the database context. This method is used to insert a new airport record into the database.
        public void Update(Airport entity) => _ctx.Airports.Update(entity); // Updates an existing airport entity in the database context. This method is used to modify an existing airport record in the database.

        public void Delete(int id) // Deletes an airport by its unique identifier.
        {

            var e = _ctx.Airports.Find(id); // Find the airport entity by its primary key (AirportId).
            if (e != null) _ctx.Airports.Remove(e);
        }

        public void Save() => _ctx.SaveChanges();


    }
}
