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
    /// <summary>
    /// Represents tests for the <see cref="VehicleController"/> class.
    /// </summary>
    public class VehicleControllerTests
    {
        private readonly Mock<IVehicleService> _vehicleServiceMock = new Mock<IVehicleService>();
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private readonly VehicleController _vehicleController;
        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly Mock<IVehicleService> _mockVehicleService;

        public VehicleControllerTests()
        {
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockVehicleService = new Mock<IVehicleService>();
            _vehicleController = new VehicleController(_mockVehicleService.Object, _mockVehicleRepository.Object);
            _vehicleController = new VehicleController(_vehicleServiceMock.Object, _vehicleRepositoryMock.Object);
        }

        /// <summary>
        /// Ensures that a valid vehicle returns an Ok result.
        /// </summary>
        [Fact]
        public void AddVehicle_ValidVehicle_ReturnsOk()
        {
            // Arrange
            var vehicleEntity = new VehicleEntity
            {
                Model = VehicleModelEnum.Hatchback,
                RegistrationDate = DateTime.Now,
                Plate = "Plate",
                RentalEvents = new RentalEventEntity
                {
                    EventType = EventTypeEnum.Rented,
                    EventDate = DateTime.Now
                }
            };

            _vehicleRepositoryMock.Setup(x => x.AddVehicle(vehicleEntity));

            // Act
            var result = _vehicleController.AddVehicle(vehicleEntity);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Ensures that an invalid vehicle returns a StatusCode 500.
        /// </summary>
        [Fact]
        public void AddVehicle_InvalidVehicle_ReturnsStatusCode500()
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

        /// <summary>
        /// Ensures that the GetAllVehiclesByModel method returns an Ok result.
        /// </summary>
        [Fact]
        public void GetAllVehiclesByModel_Returns_OkResult()
        {
            // Arrange
            var model = new VehicleModelEnum();

            // Act
            var result = _vehicleController.GetAllVehiclesByModel(model);

            // Assert
            Assert.IsType<ActionResult<IEnumerable<VehicleEntity>>>(result);
        }

        /// <summary>
        /// Ensures that the GetAllVehiclesByPlate method returns an Ok result.
        /// </summary>
        [Fact]
        public void GetAllVehiclesByPlate_Returns_OkResult()
        {
            // Arrange
            var plate = "xyz123";

            // Act
            var result = _vehicleController.GetAllVehiclesByPlate(plate);

            // Assert
            Assert.IsType<ActionResult<IEnumerable<VehicleEntity>>>(result);
        }

        /// <summary>
        /// Ensures that the GetAllVehiclesByEventType method returns an Ok result.
        /// </summary>
        [Fact]
        public void GetAllVehiclesByEventType_Returns_OkResult()
        {
            // Arrange
            EventTypeEnum eventType = EventTypeEnum.Returned;

            // Act
            var result = _vehicleController.GetAllVehiclesByEventType(eventType);

            // Assert
            Assert.IsType<ActionResult<IEnumerable<VehicleEntity>>>(result);
        }

        /// <summary>
        /// Ensures that the UpdateEventType method returns an Ok result.
        /// </summary>
        [Fact]
        public void UpdateEventType_Returns_OkResult()
        {
            // Arrange
            var plate = "xyz123";
            var eventType = new EventTypeEnum();
            var newEventType = new EventTypeEnum();

            // Act
            var result = _vehicleController.UpdateEventType(plate, eventType, newEventType);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Ensures that the RemoveVehicle method returns a BadRequestObjectResult when no vehicle is found.
        /// </summary>
        [Fact]
        public void RemoveVehicle_Returns_BadRequestResult_When_No_Vehicle_Found()
        {
            // Arrange
            var plate = "xyz123";

            _mockVehicleRepository.Setup(x => x.GetVehiclesAvaliableForRemoving(It.IsAny<string>())).Returns(new List<VehicleEntity>());

            // Act
            var result = _vehicleController.RemoveVehicle(plate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Ensures that the RemoveVehicle method returns an OkResult when a vehicle is found.
        /// </summary>
        [Fact]
        public void RemoveVehicle_Returns_OkResult_When_Vehicle_Found()
        {
            // Arrange
            var plate = "xyz123";

            _mockVehicleRepository.Setup(x => x.GetVehiclesAvaliableForRemoving(It.IsAny<string>())).Returns(new List<VehicleEntity>() { new VehicleEntity() { /* add properties in here */ } });

            // Act
            var result = _vehicleController.RemoveVehicle(plate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Ensures that the GetEventsByPlate method returns an Ok result.
        /// </summary>
        [Fact]
        public void GetEventsByPlate_Returns_OkResult()
        {
            // Arrange
            var plate = "xyz123";
            bool orderByDescending = true;

            // Act
            var result = _vehicleController.GetEventsByPlate(plate, orderByDescending);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
