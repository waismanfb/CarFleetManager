using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
        public IActionResult AddVehicle([FromBody] VehicleEntity vehicle)
        {
            try
            {
                _vehicleRepository.AddVehicle(vehicle);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAllVehiclesByModel/{model}")]
        public ActionResult<IEnumerable<VehicleEntity>> GetAllVehiclesByModel(VehicleTypeEnum model)
        {
            try
            {
                IEnumerable<VehicleEntity> validVehicles = _vehicleRepository.GetAllVehicles().Where(v => v.Model == model);
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
                IEnumerable<VehicleEntity> validVehicles = _vehicleRepository.GetAllVehicles(plate).Where(v => v.Plate == plate);
                return Ok(validVehicles);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAllVehiclesByStatus/{isRented}")]
        public ActionResult<IEnumerable<VehicleEntity>> GetAllVehiclesByPlate(bool isRented)
        {
            if (isRented == null)
                return BadRequest();

            try
            {
                IEnumerable<VehicleEntity> validVehicles = _vehicleRepository.GetAllVehicles().Where(v => v.IsRented == isRented);
                return Ok(validVehicles);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }



    }
}
