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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Calls the base implementation of OnModelCreating to ensure that any configurations defined in the base class are applied.


            modelBuilder.Entity<Booking>()  
                .HasOne(b => b.Passenger) // Represents the passenger who made the booking.
                .WithMany(p => p.Bookings) // Represents the collection of bookings made by the passenger.
                .HasForeignKey(b => b.PassengerId) // Foreign key in the Booking entity that references the Passenger entity.
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete behavior when a passenger is deleted, meaning that bookings associated with that passenger will not be automatically deleted.

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Flight) // Represents the flight associated with the booking.
                .WithMany(f => f.Bookings) // Represents the collection of bookings for that flight.
                .HasForeignKey(b => b.FlightId) // Foreign key in the Booking entity that references the Flight entity.
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete behavior when a flight is deleted, meaning that bookings associated with that flight will not be automatically deleted.



            modelBuilder.Entity<Ticket>() // Represents a ticket associated with a booking for a flight.
                .HasOne(t => t.Booking) // Represents the booking associated with the ticket.
                .WithMany(b => b.Tickets) // Represents the collection of tickets associated with the booking.
                .HasForeignKey(t => t.BookingId) // Foreign key in the Ticket entity that references the Booking entity.
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete behavior when a booking is deleted, meaning that tickets associated with that booking will not be automatically deleted.

            modelBuilder.Entity<Ticket>() // Represents a ticket associated with a flight.
                .HasOne(t => t.Flight) // Represents the flight associated with the ticket.
                .WithMany(f => f.Tickets) // Represents the collection of tickets for that flight.
                .HasForeignKey(t => t.FlightId) // Foreign key in the Ticket entity that references the Flight entity.
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete behavior when a flight is deleted, meaning that tickets associated with that flight will not be automatically deleted.


            modelBuilder.Entity<Baggage>() // Represents baggage items associated with a ticket.
                .HasOne(b => b.Passenger) // Represents the passenger who owns the baggage item.
                .WithMany(p => p.BaggageItems) // Represents the collection of baggage items owned by the passenger.
                .HasForeignKey(b => b.PassengerId) // Foreign key in the Baggage entity that references the Passenger entity.
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete behavior when a passenger is deleted, meaning that baggage items associated with that passenger will not be automatically deleted.


            modelBuilder.Entity<AircraftMaintenance>() // Represents maintenance records for aircraft.
                .HasOne(m => m.Aircraft) // Represents the aircraft associated with the maintenance record.
                .WithMany(a => a.Maintenance) // Represents the collection of maintenance records for that aircraft.
                .HasForeignKey(m => m.AircraftId) // Foreign key in the AircraftMaintenance entity that references the Aircraft entity.
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete behavior when an aircraft is deleted, meaning that maintenance records associated with that aircraft will not be automatically deleted.
        }

    }
}
