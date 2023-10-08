using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public interface IVehicleService : IVehicleRepository
    {
        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns></returns>
        IEnumerable<VehicleEntity> GetAllVehicles();

        /// <summary>
        /// Add a new vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        void AddVehicle(VehicleEntity vehicle);
    }
}
