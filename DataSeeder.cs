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


        }




    }   
}
