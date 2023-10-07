using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using Xunit;

namespace WebApiTests.Controllers
{
    public class VehicleControllerTests
    {
        private readonly Mock<IVehicleService> _vehicleServiceMock = new Mock<IVehicleService>();
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private readonly VehicleController _vehicleController;

        public VehicleControllerTests()
        {
            _vehicleController = new VehicleController(_vehicleServiceMock.Object, _vehicleRepositoryMock.Object);
        }

        [Fact]
        public async Task AddVehicle_ValidVehicle_ReturnsOk()
        {
            // Arrange
            var vehicleEntity = new VehicleEntity
            {
                Model = VehicleModelEnum.Hatchback,
                Plate = "Plate"
            };

            _vehicleRepositoryMock.Setup(x => x.AddVehicle(vehicleEntity));

            // Act
            var result = _vehicleController.AddVehicle(vehicleEntity);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddVehicle_InvalidVehicle_ReturnsStatusCode500()
        {
            // Arrange
            var vehicleEntity = new VehicleEntity
            {
                Plate = "Plate"
            };

            _vehicleRepositoryMock.Setup(x => x.AddVehicle(vehicleEntity)).Throws(new Exception("Invalid Vehicle"));

            // Act
            var result = _vehicleController.AddVehicle(vehicleEntity);

            // Assert
            var response = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, response.StatusCode);
        }
    }
}
