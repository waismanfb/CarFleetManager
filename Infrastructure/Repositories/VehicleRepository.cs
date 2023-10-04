using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDbContext _dbContext;
        private Dictionary<int, VehicleEntity> vehiclesDictionary = new Dictionary<int, VehicleEntity>();

        public VehicleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool ValidateMercosulPattern(string plate)
        {
            const string regexPattern = "^[A-Z]{3}[0-9][A-Z][0-9]{2}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(plate, regexPattern))
                throw new ArgumentException("Invalid vehicle plate format. It should follow the pattern: three uppercase letters, a number, two uppercase letters, and two numbers. Example: \"RIO2A18\" does not comply.");
            return true;
        }

        private bool ValidateVehicleType(VehicleEntity vehicle)
        {
            if (!Enum.IsDefined(typeof(VehicleTypeEnum), vehicle.Model))
                throw new ArgumentException("Invalid vehicle model");
            return true;
        }

        private bool ValidateRegistrationDate(DateTime registrationDate)
        {
            if (registrationDate <= DateTime.Today)
                throw new ArgumentException("Vehicle registration date cannot be before today's date.");
            return true;
        }

        private bool ValidatePlate(string plate)
        {
            if (string.IsNullOrEmpty(plate))
                throw new ArgumentException("The action could not be completed.");
            return true;
        }

        /// <summary>
        /// Add vehicle
        /// </summary>
        /// <param name="vehicle">Vehicle</param>
        /// <exception cref="ArgumentException">Argument exception</exception>
        public void AddVehicle(VehicleEntity vehicle)
        {
            ValidateVehicleType(vehicle);
            ValidateMercosulPattern(vehicle.Plate);
            ValidateRegistrationDate(vehicle.RegistrationDate);

            var query = @"
                INSERT INTO Vehicles (
                    Plate,
                    Model,
                    RegistrationDate,
                    IsRented
                ) VALUES (
                    @Plate,
                    @Model,
                    @RegistrationDate,
                    @IsRented
                );
            ";

            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, vehicle);
        }

        ///
        public IEnumerable<VehicleEntity> GetAllVehicles(string plate)
        {
            ValidatePlate(plate);
            ValidateMercosulPattern(plate);
            return GetAllVehicles();
        }
        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VehicleEntity> GetAllVehicles()
        {
            var query = @"
            SELECT
                v.Id,
                v.Plate,
                v.Model,
                v.RegistrationDate,
                v.IsRented,
                r.Id AS RentalEventId,
                r.EventType,
                r.EventDate
            FROM
                Vehicles v
            LEFT JOIN
                RentalEvents r ON v.Id = r.VehicleId";

            using var connection = _dbContext.CreateConnection();

            var result = connection.Query<VehicleEntity, RentalEventEntity, VehicleEntity>(query, (vehicle, rentalEvent) =>
                {
                    if (!vehiclesDictionary.TryGetValue(vehicle.Id, out var existingVehicle))
                    {
                        existingVehicle = vehicle;
                        existingVehicle.RentalEvents = new List<RentalEventEntity>();
                        vehiclesDictionary.Add(existingVehicle.Id, existingVehicle);
                    }

                    if (rentalEvent != null)
                        existingVehicle.RentalEvents.Add(rentalEvent);

                    return existingVehicle;
                },
                splitOn: "RentalEventId"
            );

            if (result == null || !result.Any())
                throw new Exception("No vehicles found.");

            return result.Distinct();
        }

    }
}
