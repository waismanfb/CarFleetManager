using Domain.Entities;
namespace Domain.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        void AddVehicle(VehicleEntity vehicle);
        IEnumerable<VehicleEntity> GetAllVehicles();

        IEnumerable<VehicleEntity> GetAllVehicles(string plate);
    }
}
