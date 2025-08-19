using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightManagementCompany.Migrations
{
    /// <inheritdoc />
    public partial class SeedStaticData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Aircraft",
                columns: new[] { "AircraftId", "Capacity", "Model", "TailNumber" },
                values: new object[,]
                {
                    { 1, 180, "A320", "HZ-A001" },
                    { 2, 396, "B777-300", "HZ-B777" }
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Country", "IATA", "Name", "TimeZone" },
                values: new object[,]
                {
                    { 1, "Jeddah", "Saudi Arabia", "JED", "King Abdulaziz Int'l", "UTC+3" },
                    { 2, "Riyadh", "Saudi Arabia", "RUH", "King Khalid Int'l", "UTC+3" },
                    { 3, "Dubai", "UAE", "DXB", "Dubai Int'l", "UTC+4" }
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "RouteId", "DestinationAirportId", "DistanceKm", "OriginAirportId" },
                values: new object[,]
                {
                    { 1, 2, 850, 1 },
                    { 2, 3, 1700, 1 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "AircraftId", "ArrivalUtc", "DepartureUtc", "FlightNumber", "RouteId", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 8, 20, 9, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 20, 8, 0, 0, 0, DateTimeKind.Utc), "SV1001", 1, "Scheduled" },
                    { 2, 2, new DateTime(2025, 8, 21, 12, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 21, 10, 0, 0, 0, DateTimeKind.Utc), "SV2002", 2, "Scheduled" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Aircraft",
                keyColumn: "AircraftId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Aircraft",
                keyColumn: "AircraftId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "RouteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "RouteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 3);
        }
    }
}
