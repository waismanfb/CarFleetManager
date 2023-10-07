using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService, IVehicleRepository vehicleRepository)
        {
            _vehicleService = vehicleService;
            _vehicleRepository = vehicleRepository;
        }


        [HttpPost("AddVehicle")]
        public IActionResult AddVehicle(VehicleEntity vehicle)
        {
            try
            {
                vehicle.RentalEvents = new RentalEventEntity(vehicle);
                _vehicleRepository.AddVehicle(vehicle);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAllVehiclesByModel/{model}")]
        public ActionResult<IEnumerable<VehicleEntity>> GetAllVehiclesByModel(VehicleModelEnum model)
        {
            try
            {
                IEnumerable<VehicleEntity> validVehicles = _vehicleRepository.GetAllVehiclesByModel(model);
                return Ok(validVehicles);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAllVehiclesByPlate/{plate}")]
        public ActionResult<IEnumerable<VehicleEntity>> GetAllVehiclesByPlate(string plate)
        {
            try
            {
                IEnumerable<VehicleEntity> validVehicles = _vehicleRepository.GetAllVehiclesByPlate(plate);
                return Ok(validVehicles);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAllVehiclesEventType/{eventType}")]
        public ActionResult<IEnumerable<VehicleEntity>> GetAllVehiclesByEventType(EventTypeEnum eventType)
        {
            try
            {
                IEnumerable<VehicleEntity> validVehicles = _vehicleRepository.GetAllVehiclesByEventType(eventType);
                return Ok(validVehicles);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateEventType/{plate}/{eventType}/{newEventType}")]
        public IActionResult UpdateEventType(string plate, EventTypeEnum eventType, EventTypeEnum newEventType)
        {
            try
            {
                _vehicleRepository.UpdateEventType(plate, eventType, newEventType);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("RemoveVehicle/{plate}")]
        public IActionResult RemoveVehicle(string plate)
        {

            IEnumerable<VehicleEntity> validVehicles = _vehicleRepository.GetVehiclesAvaliableForRemoving(plate);

            if (validVehicles.Any())
            {
                _vehicleRepository.RemoveVehicle(plate);
                return Ok();
            }
            else
            {
                return BadRequest("Could not proceed with the removal of this vehicle");
            }
            return Ok();
        }

        [HttpGet("GetEventsByPlate/{plate}/{orderByDescending?}")]
        public IActionResult GetEventsByPlate(string plate, bool orderByDescending = true)
        {
            try
            {
                IEnumerable<VehicleEntity> validVehiclesEvents = _vehicleService.GetEventsByPlate(plate, orderByDescending);
                return Ok(validVehiclesEvents);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
