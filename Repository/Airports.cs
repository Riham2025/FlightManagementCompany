using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class Airports
    {
        // Represents a repository for managing airports in the flight management system.
        private readonly FlightDbContext _ctx; // Database context for accessing airport data
        public Airports(FlightDbContext ctx) => _ctx = ctx; // Constructor to initialize the repository with a database context


       
        public List<Airport> GetAll() => // Retrieves all airports from the database.
            _ctx.Airports.AsNoTracking().ToList();// AsNoTracking() is used to improve performance by not tracking changes to the entities.


        public Airport? GetById(int id) => // Retrieves an airport by its unique identifier.
            _ctx.Airports.Find(id);// Find() is used to retrieve an entity by its primary key, which is the AirportId in this case.
    }
}
