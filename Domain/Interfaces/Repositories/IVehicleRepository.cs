using Domain.Entities;
namespace Domain.Interfaces.Repositories
{
    /// <summary>
    /// Provides the interface for the vehicle repository.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a Vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        void AddVehicle(VehicleEntity vehicle);

        /// <summary>
        /// Updates the EventType of a particular Vehicle.
        /// </summary>
        /// <param name="plate">The plate of the Vehicle to update.</param>
        /// <param name="eventType">The EventTypeEnum value to update the Vehicle to.</param>
        /// <param name="newEventType">The new EventTypeEnum value to replace the old value with.</param>
        void UpdateEventType(string plate, EventTypeEnum eventType, EventTypeEnum newEventType);

        /// <summary>
        /// Removes a Vehicle from the repository.
        /// </summary>
        /// <param name="plate">The plate of the vehicle to remove.</param>
        void RemoveVehicle(string plate);

        /// <summary>
        ///  Gets All Vehicles filtered by optional whereCondition and orderByCondition
        /// </summary>
        /// <param name="whereCondition">the where condition to filter vehicles</param>
        /// <param name="parameters">the parameters </param>
        /// <param name="orderByCondition">the order by condition</param>
        IEnumerable<VehicleEntity> GetAllVehicles(string? whereCondition = null, object? parameters = null, string? orderByCondition = null);

        /// <summary>
        /// Gets the Vehicle with a specific plate.
        /// </summary>
        /// <param name="plate">The plate of the Vehicle to get.</param>
        IEnumerable<VehicleEntity> GetAllVehicles(string plate);

        /// <summary>
        /// Gets all Rental Events of a specific Vehicle, and optionally orders them in reverse chronological order.
        /// </summary>
        /// <param name="plate">The plate of the Vehicle to get Rental Events for.</param>
        /// <param name="orderByDescending">Whether or not to sort in reverse chronological order.</param>
        IEnumerable<VehicleEntity> GetEventsByPlate(string plate, bool orderByDescending = true);

        /// <summary>
        /// Gets all Vehicles with a specific model.
        /// </summary>
        /// <param name="model">The model of the Vehicles to get.</param>
        IEnumerable<VehicleEntity> GetAllVehiclesByModel(VehicleModelEnum model);

        /// <summary>
        /// Gets the Vehicle with a specific plate.
        /// </summary>
        /// <param name="plate">The plate of the Vehicle to get.</param>
        IEnumerable<VehicleEntity> GetAllVehiclesByPlate(string plate);

        /// <summary>
        /// Gets the Vehicles that can be removed from the repository.
        /// </summary>
        /// <param name="plate">An optional plate number to exclude from the list of vehicles to remove.</param>
        IEnumerable<VehicleEntity> GetVehiclesAvaliableForRemoving(string plate);

        /// <summary>
        /// Gets all Vehicles with a specific Event Type.
        /// </summary>
        /// <param name="eventType">The EventTypeEnum value to filter the Vehicles by.</param>
        IEnumerable<VehicleEntity> GetAllVehiclesByEventType(EventTypeEnum eventType);

        /// <summary>
        /// Gets the Vehicles with a specific plate and Event Type.
        /// </summary>
        /// <param name="plate">The plate of the Vehicle(s) to get.</param>
        /// <param name="eventType">The EventTypeEnum value to filter by.</param>
        IEnumerable<VehicleEntity> GetVehiclesByPlateAndEventType(string plate, EventTypeEnum eventType);
    }
}
