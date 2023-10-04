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
        //IEnumerable<VehicleEntity> GetVehicles();

        void AddVehicle(VehicleEntity vehicle);

    }
}
