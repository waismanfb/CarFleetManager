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

        public VehicleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool ValidateMercosulPattern(string plate)
        {
            const string regexPattern = "^[A-Z]{3}[0-9][A-Z][0-9]{2}$";
            return System.Text.RegularExpressions.Regex.IsMatch(plate, regexPattern);
        }

        private bool ValidateVehicleType(VehicleEntity vehicle)
        {
            return Enum.IsDefined(typeof(VehicleTypeEnum), vehicle.Model);
        }

        private bool ValidateRegistrationDate(DateTime registrationDate)
        {
            return registrationDate >= DateTime.Now;
        }

        /// <summary>
        /// Add vehicle
        /// </summary>
        /// <param name="vehicle">Vehicle</param>
        /// <exception cref="ArgumentException">Argument exception</exception>
        public void AddVehicle(VehicleEntity vehicle)
        {
            if (!ValidateVehicleType(vehicle))
                throw new ArgumentException("Invalid vehicle model");

            if (!ValidateMercosulPattern(vehicle.Plate))
                throw new ArgumentException("Invalid vehicle plate format. It should follow the pattern: three uppercase letters, a number, two uppercase letters, and two numbers. Example: \"RIO2A18\" does not comply.");

            if (!ValidateRegistrationDate(vehicle.RegistrationDate))
                throw new ArgumentException("Vehicle registration date cannot be before today's date.");

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


    }
}
