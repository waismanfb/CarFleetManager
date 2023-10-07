using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void AddVehicle(VehicleEntity vehicle)
        {
            _vehicleRepository.AddVehicle(vehicle);
        }

        public void UpdateEventType(string plate, EventTypeEnum eventType, EventTypeEnum newEventType)
        {
            _vehicleRepository.UpdateEventType(plate, eventType, newEventType);
        }

        public IEnumerable<VehicleEntity> GetAllVehicles()
        {
            return _vehicleRepository.GetAllVehicles();
        }

        public IEnumerable<VehicleEntity> GetAllVehicles(string plate)
        {
            return _vehicleRepository.GetAllVehicles(plate);
        }

        public void RemoveVehicle(string plate)
        {
            _vehicleRepository.RemoveVehicle(plate);
        }

        public IEnumerable<VehicleEntity> GetEventsByPlate(string plate, bool orderByDescending = true)
        {
            return _vehicleRepository.GetEventsByPlate(plate, orderByDescending);
        }

        public IEnumerable<VehicleEntity> GetAllVehicles(string? whereCondition = null, object? parameters = null, string? orderByCondition = null)
        {
            return _vehicleRepository.GetAllVehicles(whereCondition, parameters, orderByCondition);
        }

        public IEnumerable<VehicleEntity> GetAllVehiclesByModel(VehicleModelEnum model)
        {
            return _vehicleRepository.GetAllVehiclesByModel(model);
        }

        public IEnumerable<VehicleEntity> GetAllVehiclesByPlate(string plate)
        {
            return _vehicleRepository.GetAllVehiclesByPlate(plate);
        }

        public IEnumerable<VehicleEntity> GetAllVehiclesByEventType(EventTypeEnum eventType)
        {
            return _vehicleRepository.GetAllVehiclesByEventType(eventType);
        }

        public IEnumerable<VehicleEntity> GetVehiclesByPlateAndEventType(string plate, EventTypeEnum eventType)
        {
            return _vehicleRepository.GetVehiclesByPlateAndEventType(plate, eventType);
        }

        public IEnumerable<VehicleEntity> GetVehiclesAvaliableForRemoving(string plate)
        {
            return _vehicleRepository.GetVehiclesAvaliableForRemoving(plate);
        }
    }
}
