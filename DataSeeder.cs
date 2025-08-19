using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany
{
    public static class DataSeeder
    {

        // Call once at app startup
        public static void Seed(FlightDbContext ctx)
        {

            // NOTE: We assume static seed (Airports/Aircraft/Routes/Flights) is already inserted by migrations.
            // Add dynamic/demo data only if tables are empty to avoid duplicates.

            if (!ctx.Passengers.Any()) 
            {
                ctx.Passengers.AddRange(
                    new Passenger { FullName = "Omar Al-Shahrani", PassportNo = "P1234567", Email = "omar@example.com", Nationality = "SA", DOB = new DateTime(1995, 3, 12) },
                    new Passenger { FullName = "Lina Al-Qahtani", PassportNo = "P7654321", Email = "lina@example.com", Nationality = "SA", DOB = new DateTime(1998, 11, 5) }
                );
                ctx.SaveChanges(); 
            }

            if (!ctx.Bookings.Any())
            {
                // Use existing seeded flight ids (1,2) and passengers we just added
                var p1 = ctx.Passengers.First(p => p.PassportNo == "P1234567"); 
                var p2 = ctx.Passengers.First(p => p.PassportNo == "P7654321"); 

                ctx.Bookings.AddRange(
                    new Booking { BookingId = 1, BookingRef = "BK000001", PassengerId = p1.PassengerId, FlightId = 1, BookingDate = DateTime.UtcNow, Status = "Confirmed" },
                    new Booking { BookingId = 2, BookingRef = "BK000002", PassengerId = p2.PassengerId, FlightId = 2, BookingDate = DateTime.UtcNow, Status = "Confirmed" }
                );
                ctx.SaveChanges();
            }

            
            
            if (!ctx.Tickets.Any())
            {
                ctx.Tickets.AddRange(
                    new Ticket { TicketId = 1, BookingId = 1, FlightId = 1, Fare = 350.00m, Seat = "12A", SeatNumber = "12A", CheckedIn = false },
                    new Ticket { TicketId = 2, BookingId = 2, FlightId = 2, Fare = 790.00m, Seat = "4C", SeatNumber = "4C", CheckedIn = false }
                );
                ctx.SaveChanges(); 
            }


            if (!ctx.Baggage.Any()) 
            {
                // Link some baggage to ticket 1 and passenger of booking 1
                var b1PassengerId = ctx.Bookings.Include(b => b.Passenger).First(b => b.BookingId == 1).PassengerId;

                ctx.Baggage.AddRange( 
                    new Baggage { BaggageId = 1, TicketId = 1, PassengerId = b1PassengerId, TagNumber = "BG123456", Weight = 18.5, WeightKg = 18.5m },
                    new Baggage { BaggageId = 2, TicketId = 1, PassengerId = b1PassengerId, TagNumber = "BG123457", Weight = 7.25, WeightKg = 7.25m }
                );
                ctx.SaveChanges();
            }

            if (!ctx.FlightCrew.Any())
            // Add flight crew only if not already seeded
            {
                ctx.FlightCrew.AddRange(
                    new FlightCrew { FlightId = 1, CrewId = 1, RoleOnFlight = "Pilot" },
                    new FlightCrew { FlightId = 1, CrewId = 2, RoleOnFlight = "Attendant" }
                );
                ctx.SaveChanges();
            }


        }




    }   
}
