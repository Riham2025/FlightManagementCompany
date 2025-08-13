using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class BookingRepository 
    {

        private readonly FlightDbContext _ctx; // Database context for accessing booking data

        public BookingRepository(FlightDbContext ctx) // Constructor to initialize the repository with a database context
        {
            _ctx = ctx; // Assign the provided context to the private field
        }

        //Get all bookings, including related Passenger and Flight
        public List<Booking> GetAll() // Retrieve all bookings from the database
        {
            return _ctx.Bookings // Represents the Bookings DbSet in the database context
                       .Include(b => b.Passenger) // Eager-load the passenger associated with the booking
                       .Include(b => b.Flight) // Eager-load the flight associated with the booking
                       .AsNoTracking() // Use AsNoTracking for performance optimization
                       .ToList(); // Converts the DbSet to a list of Booking entities.
        }

        //Find a specific booking by primary key
        public Booking? GetById(int id) // Retrieve a booking by its unique identifier
        {
            return _ctx.Bookings // Represents the Bookings DbSet in the database context
                       .Include(b => b.Passenger) // Eager-load the passenger associated with the booking
                       .Include(b => b.Flight)
                       .FirstOrDefault(b => b.BookingId == id);
        }

        public void Add(Booking entity)
        {
            _ctx.Bookings.Add(entity);
        }

        public void Update(Booking entity)
        {
            _ctx.Bookings.Update(entity);
        }

        public void Delete(int id)
        {
            var e = _ctx.Bookings.Find(id);
            if (e != null) _ctx.Bookings.Remove(e);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
