using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlightManagementCompany.Models;


namespace FlightManagementCompany
{


    public class FlightDbContext : DbContext
    {

        // Represents the database context for the flight management system.

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

            mb.Entity<Ticket>().Ignore(t => t.SeatNumber); // Ignore SeatNumber property in Ticket entity to avoid redundancy with Seat property

            mb.Entity<Ticket>().Ignore(t => t.Seat); // Ignore Seat property in Ticket entity to avoid redundancy with SeatNumber property



            base.OnModelCreating(mb);

           

            // ========== Airport ==========

            mb.Entity<Airport>() // Represents an airport in the flight management system
              .HasIndex(a => a.IATA) // Unique index on the IATA code to ensure no two airports have the same IATA code
              .IsUnique(); 

            // Route: OriginAirport -> Airport (One Airport has many OriginRoutes)
            mb.Entity<Route>() // Represents a flight route between two airports
              .HasOne(r => r.Origin) // Origin airport of the route
              .WithMany(a => a.OriginRoutes) // One airport can have many routes originating from it
              .HasForeignKey(r => r.OriginAirportId) // Foreign key for the origin airport
              .OnDelete(DeleteBehavior.Restrict);  // Restrict delete behavior to prevent deletion of an airport that has routes associated with it

            // Route: DestinationAirport -> Airport (One Airport has many DestinationRoutes)
            mb.Entity<Route>() // Represents a flight route between two airports
              .HasOne(r => r.Destination) // Destination airport of the route
              .WithMany(a => a.DestinationRoutes) // One airport can have many routes arriving at it
              .HasForeignKey(r => r.DestinationAirportId) // Foreign key for the destination airport
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of an airport that has routes associated with it

            // ========== Flight ==========
            mb.Entity<Flight>() // Represents a flight with its details
              .HasOne(f => f.Route) // Route associated with this flight
              .WithMany(r => r.Flights) // One route can have many flights
              .HasForeignKey(f => f.RouteId) // Foreign key for the route
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a route that has flights associated with it

            mb.Entity<Flight>() // Represents a flight with its details
              .HasOne(f => f.Aircraft) // Aircraft operating this flight
              .WithMany(a => a.Flights) // One aircraft can operate many flights
              .HasForeignKey(f => f.AircraftId) // Foreign key for the aircraft
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of an aircraft that has flights associated with it

            // ========== Booking ==========
            mb.Entity<Booking>() // Represents a booking made by a passenger for a flight
              .HasOne(b => b.Passenger) // Passenger who made the booking
              .WithMany(p => p.Bookings) // One passenger can have many bookings
              .HasForeignKey(b => b.PassengerId) // Foreign key for the passenger
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a passenger that has bookings associated with them

            mb.Entity<Booking>() // Represents a booking made by a passenger for a flight
              .HasOne(b => b.Flight) // Flight associated with this booking
              .WithMany(f => f.Bookings) // One flight can have many bookings
              .HasForeignKey(b => b.FlightId) // Foreign key for the flight
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a flight that has bookings associated with it

            // ========== Ticket ==========
            mb.Entity<Ticket>() // Represents a ticket booked for a flight
              .HasOne(t => t.Booking) // Booking associated with this ticket
              .WithMany(b => b.Tickets) // One booking can have many tickets
              .HasForeignKey(t => t.BookingId) // Foreign key for the booking
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a booking that has tickets associated with it

            mb.Entity<Ticket>() // Represents a ticket booked for a flight
              .HasOne(t => t.Flight) // Flight associated with this ticket
              .WithMany(f => f.Tickets) // One flight can have many tickets
              .HasForeignKey(t => t.FlightId) // Foreign key for the flight
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a flight that has tickets associated with it


            mb.Entity<Ticket>() // Represents a ticket booked for a flight
              .Property(t => t.Seat) // Seat number assigned to the ticket
              .HasMaxLength(5); // Set maximum length for the Seat property to 5 characters




            // ========== Baggage ==========
            mb.Entity<Baggage>() // Represents baggage associated with a ticket
              .HasOne(b => b.Ticket) // Ticket associated with this baggage
              .WithMany(t => t.Baggage)
              .HasForeignKey(b => b.TicketId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Baggage>()
              .HasOne(b => b.Passenger)
              .WithMany(p => p.BaggageItems)
              .HasForeignKey(b => b.PassengerId)
              .OnDelete(DeleteBehavior.Restrict);

           
           

            // ========== FlightCrew (Many-to-Many via explicit join) ==========
            mb.Entity<FlightCrew>()
              .HasKey(fc => new { fc.FlightId, fc.CrewId }); 

            mb.Entity<FlightCrew>()
              .HasOne(fc => fc.Flight)
              .WithMany(f => f.FlightCrew)
              .HasForeignKey(fc => fc.FlightId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<FlightCrew>()
              .HasOne(fc => fc.CrewMember)
              .WithMany(cm => cm.FlightCrew)
              .HasForeignKey(fc => fc.CrewId)
              .OnDelete(DeleteBehavior.Restrict);

            // ========== AircraftMaintenance ==========
            mb.Entity<AircraftMaintenance>()
              .HasOne(m => m.Aircraft)
              .WithMany(a => a.Maintenance)
              .HasForeignKey(m => m.AircraftId)
              .OnDelete(DeleteBehavior.Restrict);

            
            mb.Entity<AircraftMaintenance>()
              .Property(m => m.PerformedAtUtc)
              .HasColumnType("datetime2");

            
            mb.Entity<Aircraft>()
              .HasIndex(a => a.TailNumber)
              .IsUnique();

            mb.Entity<Passenger>()
              .HasIndex(p => p.PassportNo)
              .IsUnique();

            mb.Entity<Flight>()
              .HasIndex(f => f.FlightNumber)
              .IsUnique();


           

        }

       
        
           
           
        

    }
}
