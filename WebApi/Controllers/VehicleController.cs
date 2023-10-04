using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpGet]
        /*public IActionResult GetAllVehicles()
        {
            try
            {
                var vehicles = _vehicleService.GetVehicles();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, "Internal server error");
            }
        }*/

        [HttpPost("AddVehicle")]
        public IActionResult AddVehicle([FromBody] VehicleEntity vehicle)
        {
            try
            {
                _vehicleRepository.AddVehicle(vehicle);
                return Ok();
            }
            catch (Exception e)
            {
                // Log the exception if needed
                return StatusCode(500, e.Message);
            }
        }

    }
}
