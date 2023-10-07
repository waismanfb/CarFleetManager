using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
