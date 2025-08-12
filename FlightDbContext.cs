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
        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Baggage> Baggage { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<FlightCrew> FlightCrew { get; set; }
        public DbSet<AircraftMaintenance> AircraftMaintenance { get; set; }
    }
}
