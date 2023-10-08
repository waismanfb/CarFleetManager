using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDbContext _dbContext;
        private Dictionary<string, VehicleEntity> vehiclesDictionary = new Dictionary<String, VehicleEntity>();
        private Dictionary<string, VehicleEntity> vehicleDictionary = new Dictionary<String, VehicleEntity>();

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
            if (!Enum.IsDefined(typeof(VehicleModelEnum), vehicle.Model))
                throw new ArgumentException("Invalid vehicle model");
            return true;
        }

        private bool ValidateEventType(EventTypeEnum eventType)
        {
            if (!Enum.IsDefined(typeof(EventTypeEnum), eventType))
                throw new ArgumentException("Invalid event type");
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

        private void ValidateVehicle(VehicleEntity vehicle)
        {
            ValidateVehicleType(vehicle);
            ValidateMercosulPattern(vehicle.Plate);
            ValidateRegistrationDate(vehicle.RegistrationDate);
        }

        /// <summary>
        /// Add a vehicle to the VehicleEntity collection.
        /// </summary>
        /// <param name="vehicle">VehicleEntity to add.</param>
        /// <exception cref="ArgumentException">Thrown when the VehicleEntity does not meet validation requirements.</exception>
        public void AddVehicle(VehicleEntity vehicle)
        {
            ValidateVehicle(vehicle);

            using var connection = _dbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                connection.Execute(
                    @"INSERT INTO Vehicle (Plate, Model, RegistrationDate)
                        VALUES (@Plate, @Model, @RegistrationDate)",
                    vehicle,
                    transaction: transaction
                );

                connection.Execute(
                    @"INSERT INTO RentalEvent (VehiclePlate, EventType, EventDate)
                        VALUES (@VehiclePlate, @EventType, @EventDate)",
                    vehicle.RentalEvents,
                    transaction: transaction
                );

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Get all vehicles by model
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public IEnumerable<VehicleEntity> GetAllVehicles(string plate)
        {
            ValidatePlate(plate);
            ValidateMercosulPattern(plate);
            return GetAllVehicles();
        }

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <param name="whereCondition">WHERE condition as a string</param>
        /// <param name="parameters">Parameters for the where condition as an anonymous object</param>
        /// <param name="orderByCondition">ORDER BY condition as a string</param>
        /// <returns></returns>
        public IEnumerable<VehicleEntity> GetAllVehicles(string? whereCondition = null, object? parameters = null, string? orderByCondition = null)
        {
            var query = @"
                SELECT
                    v.Plate,
                    v.Model,
                    v.RegistrationDate,
                    r.Id AS RentalEventId,
                    r.VehiclePlate,
                    r.EventType,
                    r.EventDate
                FROM
                    Vehicle v
                INNER JOIN
                    RentalEvent r ON v.Plate = r.VehiclePlate";

            if (!string.IsNullOrEmpty(whereCondition))
                query += " WHERE " + whereCondition;

            if (!string.IsNullOrEmpty(orderByCondition))
                query += " ORDER BY " + orderByCondition;

            using var connection = _dbContext.CreateConnection();

            var result = connection.Query<VehicleEntity, RentalEventEntity, VehicleEntity>(
                query,
                (vehicle, rentalEvent) =>
                {
                    if (vehicle != null)
                    {
                        if (vehicle.RentalEvents == null)
                            vehicle.RentalEvents = new RentalEventEntity();

                        vehicle.RentalEvents.EventType = rentalEvent.EventType;
                        vehicle.RentalEvents.EventDate = rentalEvent.EventDate;
                    }

                    return vehicle;
                },
                param: parameters,
                splitOn: "RentalEventId"
            );

            if (result == null || !result.Any())
                throw new Exception("No vehicles found.");

            return result.Distinct();
        }

        /// <summary>
        /// Get vehicle by id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public VehicleEntity GetVehicleByPlate(string plate)
        {
            ValidatePlate(plate);
            ValidateMercosulPattern(plate);

            var query = @"
                        SELECT 
                            Id,
                            Plate,
                            Model,
                            RegistrationDate
                        FROM Vehicles
                        WHERE Plate = @Plate";

            using var connection = _dbContext.CreateConnection();

            var vehicle = connection.QueryFirstOrDefault<VehicleEntity>(query, new { Plate = plate });

            if (vehicle == null)
                throw new Exception($"Vehicle with plate {plate} could not be found.");

            return vehicle;

        }

        /// <summary>
        /// Gets all Vehicles with a specific plate and event type.
        /// </summary>
        /// <param name="plate">The plate number of the VehicleEntity to get.</param>
        /// <param name="eventType">The event type to filter by.</param>
        /// <returns>An IEnumerable of VehicleEntity objects that match the specified plate number and event type.</returns>
        public IEnumerable<VehicleEntity> GetVehiclesByPlateAndEventType(string plate, EventTypeEnum eventType)
        {
            ValidatePlate(plate);
            ValidateMercosulPattern(plate);
            ValidateEventType(eventType);

            return GetAllVehicles("Plate = @Plate AND EventType = @EventType", new { Plate = plate, EventType = eventType });
        }

        /// <summary>
        /// Gets Vehicles that can be removed from the repository.
        /// </summary>
        /// <param name="plate">An optional plate number to exclude from the list of vehicles to remove.</param>
        /// <returns>An IEnumerable of VehicleEntity objects that can be removed from the repository.</returns>
        public IEnumerable<VehicleEntity> GetVehiclesAvaliableForRemoving(string plate)
        {
            ValidatePlate(plate);
            ValidateMercosulPattern(plate);

            return GetAllVehicles("Plate = @Plate AND EventType <> @EventType AND DATEDIFF(DAY, v.RegistrationDate, GETDATE()) >= 15", new { Plate = plate, EventType = EventTypeEnum.Rented });

        }

        /// <summary>
        /// Updates the EventType of a VehicleEntity.
        /// </summary>
        /// <param name="plate">The plate number of the VehicleEntity to update.</param>
        /// <param name="eventType">The EventTypeEnum value to update to.</param>
        /// <param name="newEventType">The new EventTypeEnum value to replace the old value with.</param>
        public void UpdateEventType(string plate, EventTypeEnum eventType, EventTypeEnum newEventType)
        {
            IEnumerable<VehicleEntity> vehicleEntity = GetVehiclesByPlateAndEventType(plate, eventType);

            if (vehicleEntity == null)
                throw new Exception($"Vehicle with plate {plate} could not be found.");

            if (vehicleEntity.Any(r => r.RentalEvents.EventType == newEventType))
                throw new Exception($"Vehicle with plate {plate} has already the event type {eventType}.");

            var query = @"
                UPDATE RentalEvent
                SET EventType = @newEventType
                WHERE VehiclePlate = @Plate";

            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, new { newEventType, plate });
        }

        /// <summary>
        /// Removes a VehicleEntity from the repository.
        /// </summary>
        /// <param name="plate">The plate number of the VehicleEntity to remove.</param>
        public void RemoveVehicle(string plate)
        {
            ValidatePlate(plate);
            ValidateMercosulPattern(plate);

            var query = @"
                DELETE FROM RentalEvent
                WHERE VehiclePlate = @Plate;
                DELETE FROM Vehicle
                WHERE Plate = @Plate;";

            using var connection = _dbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                connection.Execute(query, new { Plate = plate }, transaction: transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Gets all Vehicles of a specific model.
        /// </summary>
        /// <param name="model">Model of the Vehicles to get.</param>
        /// <returns>An IEnumerable of VehicleEntity objects that match the specified model.</returns>
        public IEnumerable<VehicleEntity> GetAllVehiclesByModel(VehicleModelEnum model)
        {
            return GetAllVehicles("Model = @Model", new { Model = model });
        }

        /// <summary>
        /// Gets all Vehicles with a specific Event Type.
        /// </summary>
        /// <param name="eventType">The event type to filter by.</param>
        /// <returns>An IEnumerable of VehicleEntity objects that match the specified event type.</returns>
        public IEnumerable<VehicleEntity> GetAllVehiclesByEventType(EventTypeEnum eventType)
        {
            return GetAllVehicles("EventType = @EventType", new { EventType = eventType });
        }

        /// <summary>
        /// Gets all Rental events for a VehicleEntity.
        /// </summary>
        /// <param name="plate">Plate number of the vehicle to get events for.</param>
        /// <returns>An IEnumerable of VehicleEntity objects that match the specified event type and plate number.</returns>
        public IEnumerable<VehicleEntity> GetAllVehiclesByPlate(string plate)
        {
            return GetAllVehicles("Plate = @Plate", new { Plate = plate });
        }

        /// <summary>
        /// Gets all Rental events for a VehicleEntity.
        /// </summary>
        /// <param name="plate">Plate number of the vehicle to get events for.</param>
        /// <param name="orderByDescending">Whether or not to sort the events.</param>
        /// <returns>An IEnumerable of VehicleEntity objects that match the specified event type and plate number.</returns>
        public IEnumerable<VehicleEntity> GetEventsByPlate(string plate, bool orderByDescending = true)
        {
            return GetAllVehicles("Plate = @Plate", new { Plate = plate }, orderByDescending ? "r.EventDate DESC" : "r.EventDate ASC");
        }
    }
}
