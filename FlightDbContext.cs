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

            // ====== STATIC SEED (HasData) ======

            // Airports
            mb.Entity<Airport>().HasData(
               new Airport { AirportId = 1, IATA = "JED", Name = "King Abdulaziz Int'l", City = "Jeddah", Country = "Saudi Arabia", TimeZone = "UTC+3" },
               new Airport { AirportId = 2, IATA = "RUH", Name = "King Khalid Int'l", City = "Riyadh", Country = "Saudi Arabia", TimeZone = "UTC+3" },
               new Airport { AirportId = 3, IATA = "DXB", Name = "Dubai Int'l", City = "Dubai", Country = "UAE", TimeZone = "UTC+4" }
            );

            // Aircraft
            mb.Entity<Aircraft>().HasData(
                new Aircraft { AircraftId = 1, TailNumber = "HZ-A001", Model = "A320", Capacity = 180 },
                new Aircraft { AircraftId = 2, TailNumber = "HZ-B777", Model = "B777-300", Capacity = 396 }
            );

            // Routes (FK: OriginAirportId/DestinationAirportId)
            mb.Entity<Route>().HasData(
                new Route { RouteId = 1, OriginAirportId = 1, DestinationAirportId = 2, DistanceKm = 850 },
                new Route { RouteId = 2, OriginAirportId = 1, DestinationAirportId = 3, DistanceKm = 1700 }
            );

            // Flights (FK: RouteId, AircraftId)
            // Use UTC times


            var dep1 = new DateTime(2025, 08, 20, 08, 00, 00, DateTimeKind.Utc);
            var arr1 = new DateTime(2025, 08, 20, 09, 30, 00, DateTimeKind.Utc);
            var dep2 = new DateTime(2025, 08, 21, 10, 00, 00, DateTimeKind.Utc);
            var arr2 = new DateTime(2025, 08, 21, 12, 45, 00, DateTimeKind.Utc);

            mb.Entity<Flight>().HasData(
                 new Flight { FlightId = 1, FlightNumber = "SV1001", RouteId = 1, AircraftId = 1, DepartureUtc = dep1, ArrivalUtc = arr1, Status = "Scheduled" },
                 new Flight { FlightId = 2, FlightNumber = "SV2002", RouteId = 2, AircraftId = 2, DepartureUtc = dep2, ArrivalUtc = arr2, Status = "Scheduled" }
            );



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
              .WithMany(t => t.Baggage) // One ticket can have many baggage items
              .HasForeignKey(b => b.TicketId) // Foreign key for the ticket
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a ticket that has baggage associated with it

            mb.Entity<Baggage>() // Represents baggage associated with a passenger
              .HasOne(b => b.Passenger) // Passenger who owns this baggage
              .WithMany(p => p.BaggageItems) // One passenger can have many baggage items
              .HasForeignKey(b => b.PassengerId) // Foreign key for the passenger
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a passenger that has baggage associated with them




            // ========== FlightCrew (Many-to-Many via explicit join) ==========
            mb.Entity<FlightCrew>() // Represents the assignment of crew members to flights
              .HasKey(fc => new { fc.FlightId, fc.CrewId }); // Composite key consisting of FlightId and CrewId

            mb.Entity<FlightCrew>() // Represents the assignment of crew members to flights
              .HasOne(fc => fc.Flight) // Flight associated with this crew assignment
              .WithMany(f => f.FlightCrew) // One flight can have many crew assignments
              .HasForeignKey(fc => fc.FlightId) // Foreign key for the flight
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a flight that has crew assignments associated with it

            mb.Entity<FlightCrew>() // Represents the assignment of crew members to flights
              .HasOne(fc => fc.CrewMember) // Crew member assigned to this flight
              .WithMany(cm => cm.FlightCrew) // One crew member can be assigned to many flights
              .HasForeignKey(fc => fc.CrewId) // Foreign key for the crew member
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of a crew member that has flight assignments associated with them

            // ========== AircraftMaintenance ==========
            mb.Entity<AircraftMaintenance>() // Represents maintenance records for aircraft
              .HasOne(m => m.Aircraft) // Aircraft associated with this maintenance record
              .WithMany(a => a.Maintenance) // One aircraft can have many maintenance records
              .HasForeignKey(m => m.AircraftId) // Foreign key for the aircraft
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior to prevent deletion of an aircraft that has maintenance records associated with it


            mb.Entity<AircraftMaintenance>() // Represents maintenance records for aircraft
              .Property(m => m.PerformedAtUtc) // Date and time when the maintenance was performed
              .HasColumnType("datetime2"); // Set the column type for PerformedAtUtc to datetime2 for better precision


            mb.Entity<Aircraft>() // Represents an aircraft in the flight management system
              .HasIndex(a => a.TailNumber) // Unique index on the TailNumber to ensure no two aircraft have the same tail number
              .IsUnique(); 

            mb.Entity<Passenger>() // Represents a passenger in the flight management system
              .HasIndex(p => p.PassportNo) // Unique index on the PassportNo to ensure no two passengers have the same passport number
              .IsUnique();

            mb.Entity<Flight>() // Represents a flight with its details
              .HasIndex(f => f.FlightNumber) // Unique index on the FlightNumber to ensure no two flights have the same flight number
              .IsUnique();


           

        }

       
        
           
           
        

    }
}
