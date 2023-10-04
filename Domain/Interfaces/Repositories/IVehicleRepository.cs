using Domain.Entities;
namespace Domain.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        void AddVehicle(VehicleEntity vehicle);
    }
}
