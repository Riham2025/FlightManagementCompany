using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Services;
using FlightManagementCompany.Repository;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany
{
    public static class DataSeeder
    {

        // Call once at app startup
        
            public static void CreateSampleData(
            FlightDbContext db,
            IAirports airportRepo,
            IRouteRepository routeRepo,
            IAircraftRepository aircraftRepo,
            IFlightRepository flightRepo,
            IPassengerRepository passengerRepo,
            IBookingRepository bookingRepo,
            ITicketRepository ticketRepo,
            IBaggageRepository baggageRepo,
            ICrewMemberRepository crewRepo,
            IFlightCrewRepository flightCrewRepo,
            IAircraftMaintenanceRepository maintenanceRepo
        )
        {

            // NOTE: We assume static seed (Airports/Aircraft/Routes/Flights) is already inserted by migrations.
            // Add dynamic/demo data only if tables are empty to avoid duplicates.

            if (!passengerRepo.GetAll().Any())
            {
                var passengers = new List<Passenger>();
                {
                    new Passenger { FullName = "Omar Al-Shahrani", PassportNo = "P1234567", Email = "omar@example.com", Nationality = "SA", DOB = new DateTime(1995, 3, 12) };
                    new Passenger { FullName = "Lina Al-Qahtani", PassportNo = "P7654321", Email = "lina@example.com", Nationality = "SA", DOB = new DateTime(1998, 11, 5) };
                }
                foreach (var a in passengers) passengerRepo.Add(a);
                db.SaveChanges();
                
            }

            if (!bookingRepo.GetAll().Any())
            {
                // Use existing seeded flight ids (1,2) and passengers we just added
                var passengers = new List<Passenger>();
                var p1 = passengers.First(p => p.PassportNo == "P1234567");
                var p2 = passengers.First(p => p.PassportNo == "P7654321");

                var bookings = new List<Booking>();
                {
                    new Booking { BookingRef = "BK000001", PassengerId = p1.PassengerId, FlightId = 1, BookingDate = DateTime.UtcNow, Status = "Confirmed" };
                    new Booking { BookingRef = "BK000002", PassengerId = p2.PassengerId, FlightId = 2, BookingDate = DateTime.UtcNow, Status = "Confirmed" };
                }
                foreach (var b in bookings) bookingRepo.Add(b);
                db.SaveChanges();
            }



            if (!ticketRepo.GetAll().Any())
            {
                var bookings = db.Bookings.ToList();

                // Use existing booking and flight ids (1,2) to create tickets
                var tickets = new List<Ticket>();
                {
                    new Ticket { BookingId = bookings[0].BookingId, FlightId = 1, Fare = 350.00m, Seat = "12A", SeatNumber = "12A", CheckedIn = false };
                    new Ticket { BookingId = bookings[1].BookingId, FlightId = 2, Fare = 790.00m, Seat = "4C", SeatNumber = "4C", CheckedIn = false };

                }
                foreach (var t in tickets) ticketRepo.Add(t);
                db.SaveChanges();
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
                    // Assuming flight 1 has a pilot and an attendant
                    new FlightCrew { FlightId = 1, CrewId = 1, RoleOnFlight = "Pilot" },
                    new FlightCrew { FlightId = 1, CrewId = 2, RoleOnFlight = "Attendant" }
                );
                ctx.SaveChanges();
            }

            if (!ctx.AircraftMaintenance.Any())
            // Add aircraft maintenance records only if not already seeded
            {
                ctx.AircraftMaintenance.AddRange(
                    // Assuming aircraft 1 and 2 have maintenance records
                    new AircraftMaintenance { MaintenanceId = 1, AircraftId = 1, Description = "A-Check complete", PerformedAtUtc = DateTime.UtcNow.AddDays(-5), Type = "A-Check" },
                    new AircraftMaintenance { MaintenanceId = 2, AircraftId = 2, Description = "Engine inspection", PerformedAtUtc = DateTime.UtcNow.AddDays(-15), Type = "B-Check" }
                );
                ctx.SaveChanges();
            }


        }
    }





} 

