using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller for managing vehicles in the fleet.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// VehicleController constructor.
        /// </summary>
        /// <param name="vehicleService">Vechicle service</param>
        /// <param name="vehicleRepository">Vehicle repository</param>
        public VehicleController(IVehicleService vehicleService, IVehicleRepository vehicleRepository)
        {
            _vehicleService = vehicleService;
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Endpoint for adding a new vehicle to the fleet.
        /// </summary>
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

        /// <summary>
        /// Endpoint for retrieving all vehicles in the fleet of the specified model.
        /// </summary>
        /// <param name="model">The model of the vehicles to retrieve.</param>
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

        /// <summary>
        /// Endpoint for retrieving all vehicles in the fleet with the specified license plate.
        /// </summary>
        /// <param name="plate">The license plate of the vehicles to retrieve.</param>
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

        /// <summary>
        /// Endpoint for retrieving all vehicles in the fleet of the specified event type.
        /// </summary>
        /// <param name="eventType">The event type of the vehicles to retrieve.</param>
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

        /// <summary>
        /// Endpoint for updating the event type of a vehicle with the specified license plate.
        /// </summary>
        /// <param name="plate">The license plate of the vehicle to update.</param>
        /// <param name="eventType">The current event type of the vehicle to update.</param>
        /// <param name="newEventType">The new event type for the vehicle.</param>
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

        /// <summary>
        /// Endpoint for removing a vehicle from the fleet with the specified license plate.
        /// </summary>
        /// <param name="plate">The license plate of the vehicle to remove.</param>
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
        }

        /// <summary>
        /// Endpoint for retrieving all rental events of a vehicle with the specified license plate.
        /// </summary>
        /// <param name="plate">The license plate of the vehicle to retrieve rental events for.</param>
        /// <param name="orderByDescending">Whether or not to order the rental events by descending date (default: true).</param>
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
