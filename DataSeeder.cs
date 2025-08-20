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

        // Safe to call many times — it only inserts when empty
        public static void Seed(FlightDbContext db)
        {
            // Make sure the database exists (Migrate() already does this, but harmless)
            db.Database.EnsureCreated();

            // If we already have airports we assume the DB is seeded
            if (db.Airports.Any()) return;

            // ===== Airports =====
            var jfk = new Airport { IATA = "JFK", Name = "John F. Kennedy", City = "New York", Country = "USA", TimeZone = "UTC-5" };
            var lax = new Airport { IATA = "LAX", Name = "Los Angeles Intl", City = "Los Angeles", Country = "USA", TimeZone = "UTC-8" };
            db.Airports.AddRange(jfk, lax);
            db.SaveChanges();

            // ===== Route =====
            var r1 = new Route { Origin = jfk, Destination = lax, DistanceKm = 3974 };
            db.Routes.Add(r1);
            db.SaveChanges();

            // ===== Aircraft =====
            var a1 = new Aircraft { TailNumber = "N123AA", Model = "B737-800", Capacity = 180 };
            db.Aircraft.Add(a1);
            db.SaveChanges();

            // ===== Flight =====
            var f1 = new Flight
            {
                FlightNumber = "AA100",
                Route = r1,
                Aircraft = a1,
                DepartureUtc = DateTime.UtcNow.AddDays(1).Date.AddHours(12),
                ArrivalUtc = DateTime.UtcNow.AddDays(1).Date.AddHours(12 + 6)
            };
            db.Flights.Add(f1);
            db.SaveChanges();

            // ===== Passenger =====
            var p1 = new Passenger { FullName = "Alice Smith", PassportNo = "P1234567", Email = "alice@example.com" };
            db.Passengers.Add(p1);
            db.SaveChanges();

            // ===== Booking =====
            var b1 = new Booking
            {
                Passenger = p1,
                Flight = f1,
                BookingRef = "BK001",
                BookingDate = DateTime.UtcNow,
                Status = "Confirmed"
            };
            db.Bookings.Add(b1);
            db.SaveChanges();

            // ===== Ticket =====
            var t1 = new Ticket
            {
                Booking = b1,
                Flight = f1,
                Fare = 350m,
                Seat = "12A",     // you ignored SeatNumber in the model, so we use Seat
                CheckedIn = false
            };
            db.Tickets.Add(t1);
            db.SaveChanges();

            // ===== Baggage =====
            db.Baggage.Add(new Baggage { Ticket = t1, Passenger = p1, TagNumber = "BG001", Weight = 20.0 });
            db.SaveChanges();

            // ===== Crew =====
            var c1 = new CrewMember { FullName = "John Pilot", Role = "Pilot" };
            db.CrewMembers.Add(c1);
            db.SaveChanges();

            db.FlightCrew.Add(new FlightCrew { Flight = f1, CrewMember = c1, RoleOnFlight = "Pilot" });
            db.SaveChanges();

            // ===== Maintenance =====
            db.AircraftMaintenance.Add(new AircraftMaintenance
            {
                Aircraft = a1,
                Description = "A-Check",
                PerformedAtUtc = DateTime.UtcNow,
                Type = "A-Check",
                Notes = "Initial service"
            });
            db.SaveChanges();
        }



        //// Call once at app startup

        //    public static void CreateSampleData(
        //    FlightDbContext db,
        //    IAirports airportRepo,
        //    IRouteRepository routeRepo,
        //    IAircraftRepository aircraftRepo,
        //    IFlightRepository flightRepo,
        //    IPassengerRepository passengerRepo,
        //    IBookingRepository bookingRepo,
        //    ITicketRepository ticketRepo,
        //    IBaggageRepository baggageRepo,
        //    ICrewMemberRepository crewRepo,
        //    IFlightCrewRepository flightCrewRepo,
        //    IAircraftMaintenanceRepository maintenanceRepo,
        //    IAircraftMaintenanceRepository AMRepo
        //)
        //{

        //    // NOTE: We assume static seed (Airports/Aircraft/Routes/Flights) is already inserted by migrations.
        //    // Add dynamic/demo data only if tables are empty to avoid duplicates.

        //    if (!passengerRepo.GetAll().Any())
        //    {
        //        var passengers = new List<Passenger>();
        //        {
        //            new Passenger { FullName = "Omar Al-Shahrani", PassportNo = "P1234567", Email = "omar@example.com", Nationality = "SA", DOB = new DateTime(1995, 3, 12) };
        //            new Passenger { FullName = "Lina Al-Qahtani", PassportNo = "P7654321", Email = "lina@example.com", Nationality = "SA", DOB = new DateTime(1998, 11, 5) };
        //        }
        //        foreach (var a in passengers) passengerRepo.Add(a);
        //        db.SaveChanges();

        //    }

        //    if (!bookingRepo.GetAll().Any())
        //    {
        //        // Use existing seeded flight ids (1,2) and passengers we just added
        //        var passengers = new List<Passenger>();
        //        var p1 = passengers.First(p => p.PassportNo == "P1234567");
        //        var p2 = passengers.First(p => p.PassportNo == "P7654321");

        //        var bookings = new List<Booking>();
        //        {
        //            new Booking { BookingRef = "BK000001", PassengerId = p1.PassengerId, FlightId = 1, BookingDate = DateTime.UtcNow, Status = "Confirmed" };
        //            new Booking { BookingRef = "BK000002", PassengerId = p2.PassengerId, FlightId = 2, BookingDate = DateTime.UtcNow, Status = "Confirmed" };
        //        }
        //        foreach (var b in bookings) bookingRepo.Add(b);
        //        db.SaveChanges();
        //    }



        //    if (!ticketRepo.GetAll().Any())
        //    {
        //        var bookings = db.Bookings.ToList();

        //        // Use existing booking and flight ids (1,2) to create tickets
        //        var tickets = new List<Ticket>();
        //        {
        //            new Ticket { BookingId = bookings[0].BookingId, FlightId = 1, Fare = 350.00m, Seat = "12A", SeatNumber = "12A", CheckedIn = false };
        //            new Ticket { BookingId = bookings[1].BookingId, FlightId = 2, Fare = 790.00m, Seat = "4C", SeatNumber = "4C", CheckedIn = false };

        //        }
        //        foreach (var t in tickets) ticketRepo.Add(t);
        //        db.SaveChanges();
        //    }


        //    if (!baggageRepo.GetAll().Any())
        //    {
        //        // Link some baggage to ticket 1 and passenger of booking 1
        //        var Baggage = db.Baggage.ToList();
        //        var passengesrs = new List<Passenger>();

        //        var baggage = new List<Baggage>();
        //        {
        //            new Baggage { BaggageId = 1, TicketId = 1, PassengerId = passengesrs[0].PassengerId, TagNumber = "BG123456", Weight = 18.5, WeightKg = 18.5m };
        //            new Baggage { BaggageId = 2, TicketId = 1, PassengerId = passengesrs[1].PassengerId, TagNumber = "BG123457", Weight = 7.25, WeightKg = 7.25m };
        //        }
        //        foreach (var b in baggage) baggageRepo.Add(b);
        //        db.SaveChanges();
        //    }

        //    // FLIGHT-CREW ASSIGNMENTS
        //    if (!flightCrewRepo.GetAll().Any())
        //    {
        //        var flights = flightRepo.GetAll();
        //        var crew = crewRepo.GetAll();
        //        if (flights.Count >= 1 && crew.Count >= 2)
        //        {
        //            flightCrewRepo.Add(new FlightCrew { FlightId = flights[0].FlightId, CrewId = crew[0].CrewId, RoleOnFlight = "Pilot" });
        //            flightCrewRepo.Add(new FlightCrew { FlightId = flights[0].FlightId, CrewId = crew[1].CrewId, RoleOnFlight = "Attendant" });
        //            flightCrewRepo.Save();
        //        }
        //    }

        //    if (!maintenanceRepo.GetAll().Any())
        //    {
        //        var aircraft = aircraftRepo.GetAll();
        //        if (aircraft.Count >= 2)
        //        {
        //            maintenanceRepo.Add(new AircraftMaintenance { AircraftId = aircraft[0].AircraftId, Description = "A-Check complete", Type = "A-Check", PerformedAtUtc = DateTime.UtcNow.AddDays(-5) });
        //            maintenanceRepo.Add(new AircraftMaintenance { AircraftId = aircraft[1].AircraftId, Description = "Engine inspection", Type = "B-Check", PerformedAtUtc = DateTime.UtcNow.AddDays(-15) });
        //            maintenanceRepo.Save();
        //        }
        //    }

        //  }



    }
} 

