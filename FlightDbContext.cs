using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;

namespace FlightManagementCompany
{
     public class FlightDbContext : DbContext
    {

        // DbSets
        public DbSet<Airport> Airports { get; set; } // Represents a collection of airports in the database
        public DbSet<Aircraft> Aircraft { get; set; } // Represents a collection of aircraft in the database
        public DbSet<Route> Routes { get; set; } // Represents a collection of flight routes in the database
        public DbSet<Flight> Flights { get; set; } // Represents a collection of flights in the database
        public DbSet<Passenger> Passengers { get; set; } // Represents a collection of passengers in the database
        public DbSet<Booking> Bookings { get; set; } // Represents a collection of bookings in the database
        public DbSet<Ticket> Tickets { get; set; } // Represents a collection of tickets in the database
        public DbSet<Baggage> Baggage { get; set; } // Represents a collection of baggage items in the database
        public DbSet<CrewMember> CrewMembers { get; set; } // Represents a collection of crew members in the database
        public DbSet<FlightCrew> FlightCrew { get; set; } // Represents a collection of flight crew assignments in the database
        public DbSet<AircraftMaintenance> AircraftMaintenance { get; set; } // Represents a collection of aircraft maintenance records in the database


        protected override void OnConfiguring(DbContextOptionsBuilder options) // This method is used to configure the database context
        {
            // Configure the database connection
            options.UseSqlServer("Server=.;Database=FlightDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
