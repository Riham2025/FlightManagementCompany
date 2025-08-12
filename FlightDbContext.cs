using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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



        // This method is called when the model for a derived context is being created.
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Airport
            mb.Entity<Airport>() // Represents the Airport entity
              .HasIndex(a => a.IATA).IsUnique(); // Ensure IATA code is unique
            mb.Entity<Airport>() // Represents the Airport entity
              .Property(a => a.IATA).IsRequired().HasMaxLength(3); // Ensure IATA code is required and has a maximum length of 3 characters

            // Aircraft
            mb.Entity<Aircraft>() // Represents the Aircraft entity
              .HasIndex(a => a.TailNumber).IsUnique(); // Ensure TailNumber is unique
            mb.Entity<Aircraft>() // Represents the Aircraft entity
              .Property(a => a.TailNumber).IsRequired(); // Ensure TailNumber is required

            // Route (Airport as Origin & Destination)
            mb.Entity<Route>()// Represents the Route entity
              .HasOne(r => r.Origin).WithMany()
              .HasForeignKey(r => r.OriginAirportId) // Foreign key to the origin airport
              .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete on origin airport
            mb.Entity<Route>() // Represents the Route entity
              .HasOne(r => r.Destination).WithMany() // Navigation property for destination airport
              .HasForeignKey(r => r.DestinationAirportId) // Foreign key to the destination airport
              .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete on destination airport

            // Flight
            mb.Entity<Flight>() // Represents the Flight entity
              .Property(f => f.FlightNumber).IsRequired().HasMaxLength(10); // Ensure FlightNumber is required and has a maximum length of 10 characters
            // Unique (FlightNumber + Departure Date only)
            mb.Entity<Flight>() // Represents the Flight entity
              .HasIndex(f => new { f.FlightNumber, f.DepartureUtc }) // Ensure uniqueness of FlightNumber and DepartureUtc
              .IsUnique(); // Ensure that the combination of FlightNumber and DepartureUtc is unique
            mb.Entity<Flight>() // Represents the Flight entity
              .HasOne(f => f.Route).WithMany(r => r.Flights) // Navigation property for the route associated with the flight
              .HasForeignKey(f => f.RouteId); // Foreign key to the route
            mb.Entity<Flight>() // Represents the Flight entity
              .HasOne(f => f.Aircraft).WithMany(a => a.Flights) // Navigation property for the aircraft operating the flight
              .HasForeignKey(f => f.AircraftId); // Foreign key to the aircraft

            // Passenger
            mb.Entity<Passenger>()// Represents the Passenger entity
              .HasIndex(p => p.PassportNo).IsUnique(); // Ensure PassportNo is unique
            mb.Entity<Passenger>() // Represents the Passenger entity
              .Property(p => p.FullName).IsRequired().HasMaxLength(80); // Ensure FullName is required and has a maximum length of 80 characters

            // Booking
            mb.Entity<Booking>() //
              .HasIndex(b => b.BookingRef).IsUnique();
            mb.Entity<Booking>()
              .HasOne(b => b.Passenger).WithMany(p => p.Bookings)
              .HasForeignKey(b => b.PassengerId);

            // Ticket
            mb.Entity<Ticket>()
              .Property(t => t.Fare).HasColumnType("decimal(10,2)");
            mb.Entity<Ticket>()
              .HasOne(t => t.Booking).WithMany(b => b.Tickets)
              .HasForeignKey(t => t.BookingId);
            mb.Entity<Ticket>()
              .HasOne(t => t.Flight).WithMany(f => f.Tickets)
              .HasForeignKey(t => t.FlightId);

            // Baggage
            mb.Entity<Baggage>()
              .Property(b => b.WeightKg).HasColumnType("decimal(6,2)");
            mb.Entity<Baggage>()
              .HasOne(b => b.Ticket).WithMany(t => t.Baggage)
              .HasForeignKey(b => b.TicketId);

            // CrewMember
            mb.Entity<CrewMember>()
              .Property(c => c.FullName).IsRequired().HasMaxLength(80);

            // FlightCrew (composite key FlightId + CrewId)
            mb.Entity<FlightCrew>()
              .HasKey(fc => new { fc.FlightId, fc.CrewId });
            mb.Entity<FlightCrew>()
              .HasOne(fc => fc.Flight).WithMany(f => f.FlightCrew)
              .HasForeignKey(fc => fc.FlightId);
            mb.Entity<FlightCrew>()
              .HasOne(fc => fc.CrewMember).WithMany(c => c.FlightCrew)
              .HasForeignKey(fc => fc.CrewId);

            // AircraftMaintenance
            mb.Entity<AircraftMaintenance>()
              .HasOne(m => m.Aircraft).WithMany(a => a.Maintenance)
              .HasForeignKey(m => m.AircraftId);
        }

    }
}
