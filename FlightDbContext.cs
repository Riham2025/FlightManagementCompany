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
            mb.Entity<Airport>()
              .HasIndex(a => a.IATA).IsUnique();
            mb.Entity<Airport>()
              .Property(a => a.IATA).IsRequired().HasMaxLength(3);

            // Aircraft
            mb.Entity<Aircraft>()
              .HasIndex(a => a.TailNumber).IsUnique();
            mb.Entity<Aircraft>()
              .Property(a => a.TailNumber).IsRequired();

            // Route (Airport as Origin & Destination)
            mb.Entity<Route>()
              .HasOne(r => r.Origin).WithMany()
              .HasForeignKey(r => r.OriginAirportId)
              .OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Route>()
              .HasOne(r => r.Destination).WithMany()
              .HasForeignKey(r => r.DestinationAirportId)
              .OnDelete(DeleteBehavior.Restrict);

            // Flight
            mb.Entity<Flight>()
              .Property(f => f.FlightNumber).IsRequired().HasMaxLength(10);
            // Unique (FlightNumber + Departure Date only)
            mb.Entity<Flight>()
              .HasIndex(f => new { f.FlightNumber, f.DepartureUtc })
              .IsUnique();
            mb.Entity<Flight>()
              .HasOne(f => f.Route).WithMany(r => r.Flights)
              .HasForeignKey(f => f.RouteId);
            mb.Entity<Flight>()
              .HasOne(f => f.Aircraft).WithMany(a => a.Flights)
              .HasForeignKey(f => f.AircraftId);

            // Passenger
            mb.Entity<Passenger>()
              .HasIndex(p => p.PassportNo).IsUnique();
            mb.Entity<Passenger>()
              .Property(p => p.FullName).IsRequired().HasMaxLength(80);

            // Booking
            mb.Entity<Booking>()
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
