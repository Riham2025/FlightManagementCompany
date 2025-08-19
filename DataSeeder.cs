using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;

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


        }




    }   
}
