using Domain.Entities;
namespace Domain.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        void AddVehicle(VehicleEntity vehicle);
        void UpdateEventType(string plate, EventTypeEnum eventType, EventTypeEnum newEventType);
        void RemoveVehicle(string plate);
        IEnumerable<VehicleEntity> GetAllVehicles(string? whereCondition = null, object? parameters = null, string? orderByCondition = null);
        IEnumerable<VehicleEntity> GetAllVehicles(string plate);
        IEnumerable<VehicleEntity> GetEventsByPlate(string plate, bool orderByDescending = true);
        IEnumerable<VehicleEntity> GetAllVehiclesByModel(VehicleModelEnum model);
        IEnumerable<VehicleEntity> GetAllVehiclesByPlate(string plate);
        IEnumerable<VehicleEntity> GetVehiclesAvaliableForRemoving(string plate);
        IEnumerable<VehicleEntity> GetAllVehiclesByEventType(EventTypeEnum eventType);
        IEnumerable<VehicleEntity> GetVehiclesByPlateAndEventType(string plate, EventTypeEnum eventType);
    }
}
