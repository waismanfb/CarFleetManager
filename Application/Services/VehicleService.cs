using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    /// <summary>
    /// Represents the service for managing the vehicles and their events.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// VehicleService constructor.
        /// </summary>
        /// <param name="vehicleRepository">The repository for the vehicles.</param>
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle entity to add.</param>
        public void AddVehicle(VehicleEntity vehicle)
        {
            _vehicleRepository.AddVehicle(vehicle);
        }

        /// <summary>
        /// Updates the event type of a vehicle.
        /// </summary>
        /// <param name="plate">The plate of the vehicle to update.</param>
        /// <param name="eventType">The type of the event to update.</param>
        /// <param name="newEventType">The new type of the event.</param>
        public void UpdateEventType(string plate, EventTypeEnum eventType, EventTypeEnum newEventType)
        {
            _vehicleRepository.UpdateEventType(plate, eventType, newEventType);
        }

        /// <summary>
        /// Removes a vehicle from the repository.
        /// </summary>
        /// <param name="plate">The plate of the vehicle to remove.</param>
        public void RemoveVehicle(string plate)
        {
            _vehicleRepository.RemoveVehicle(plate);
        }

        /// <summary>
        /// Gets all the vehicles from the repository.
        /// </summary>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetAllVehicles()
        {
            return _vehicleRepository.GetAllVehicles();
        }

        /// <summary>
        /// Gets all the vehicles from the repository that match the given plate.
        /// </summary>
        /// <param name="plate">The plate to filter by.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetAllVehicles(string plate)
        {
            return _vehicleRepository.GetAllVehicles(plate);
        }

        /// <summary>
        /// Gets the events of a vehicle sorted by date.
        /// </summary>
        /// <param name="plate">The plate of the vehicle to get the events of.</param>
        /// <param name="orderByDescending">Determines whether to sort the events in descending order. Default is true.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetEventsByPlate(string plate, bool orderByDescending = true)
        {
            return _vehicleRepository.GetEventsByPlate(plate, orderByDescending);
        }

        /// <summary>
        /// Gets all the vehicles from the repository based on the given conditions.
        /// </summary>
        /// <param name="whereCondition">The WHERE clause of the SQL query.</param>
        /// <param name="parameters">The parameters to replace in the SQL query.</param>
        /// <param name="orderByCondition">The ORDER BY clause of the SQL query.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetAllVehicles(string? whereCondition = null, object? parameters = null, string? orderByCondition = null)
        {
            return _vehicleRepository.GetAllVehicles(whereCondition, parameters, orderByCondition);
        }

        /// <summary>
        /// Gets all the vehicles from the repository based on the given model.
        /// </summary>
        /// <param name="model">The model to filter by.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetAllVehiclesByModel(VehicleModelEnum model)
        {
            return _vehicleRepository.GetAllVehiclesByModel(model);
        }

        /// <summary>
        /// Gets all the vehicles from the repository that match the given plate.
        /// </summary>
        /// <param name="plate">The plate to filter by.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetAllVehiclesByPlate(string plate)
        {
            return _vehicleRepository.GetAllVehiclesByPlate(plate);
        }

        /// <summary>
        /// Gets all the vehicles from the repository that match the given event type.
        /// </summary>
        /// <param name="eventType">The event type to filter by.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetAllVehiclesByEventType(EventTypeEnum eventType)
        {
            return _vehicleRepository.GetAllVehiclesByEventType(eventType);
        }

        /// <summary>
        /// Gets all the vehicles from the repository that match the given plate and event type.
        /// </summary>
        /// <param name="plate">The plate to filter by.</param>
        /// <param name="eventType">The event type to filter by.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetVehiclesByPlateAndEventType(string plate, EventTypeEnum eventType)
        {
            return _vehicleRepository.GetVehiclesByPlateAndEventType(plate, eventType);
        }

        /// <summary>
        /// Gets all the vehicles that are available for removal, based on the given plate.
        /// </summary>
        /// <param name="plate">The plate of the vehicle that needs to be removed.</param>
        /// <returns>An IEnumerable of VehicleEntity objects.</returns>
        public IEnumerable<VehicleEntity> GetVehiclesAvaliableForRemoving(string plate)
        {
            return _vehicleRepository.GetVehiclesAvaliableForRemoving(plate);
        }
    }
}