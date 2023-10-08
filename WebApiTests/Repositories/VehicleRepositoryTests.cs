using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Moq;

namespace WebApiTests.Repositories
{
    public class VehicleRepositoryTests
    {
        private readonly Mock<IDbContext> _dbContextMock;
        private readonly Mock<VehicleRepository> _vehicleRepositoryMock;
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepositoryTests"/> class.
        /// </summary>
        public VehicleRepositoryTests()
        {
            _dbContextMock = new Mock<IDbContext>();
            _repository = new VehicleRepository(_dbContextMock.Object);
            _vehicleRepositoryMock = new Mock<VehicleRepository>(_dbContextMock.Object);
        }

        /// <summary>
        /// Tests that the <see cref="VehicleRepository.AddVehicle"/> method throws an <see cref="ArgumentException"/> 
        /// exception when the vehicle model is invalid.
        /// </summary>
        [Fact]
        public void AddVehicle_ThrowsArgumentException_WhenInvalidVehicleModel()
        {
            var invalidVehicle = new VehicleEntity
            {
                Model = (VehicleModelEnum)100,
                Plate = "ABC1234",
                RegistrationDate = DateTime.Today.AddDays(1)
            };

            Assert.Throws<ArgumentException>(() => _repository.AddVehicle(invalidVehicle));
        }

        /// <summary>
        /// Tests that the <see cref="VehicleRepository.AddVehicle"/> method throws an <see cref="ArgumentException"/> 
        /// exception when the vehicle plate is invalid.
        /// </summary>
        [Fact]
        public void AddVehicle_ThrowsArgumentException_WhenInvalidPlate()
        {
            var invalidVehicle = new VehicleEntity
            {
                Model = VehicleModelEnum.Hatchback,
                Plate = "1ABC234",
                RegistrationDate = DateTime.Today.AddDays(1)
            };

            Assert.Throws<ArgumentException>(() => _repository.AddVehicle(invalidVehicle));
        }

        /// <summary>
        /// Tests that the <see cref="VehicleRepository.AddVehicle"/> method throws an <see cref="ArgumentException"/> 
        /// exception when the registration date is invalid.
        /// </summary>
        [Fact]
        public void AddVehicle_ThrowsArgumentException_WhenInvalidRegistrationDate()
        {
            var invalidVehicle = new VehicleEntity
            {
                Model = VehicleModelEnum.Sedan,
                Plate = "ABC1234",
                RegistrationDate = DateTime.Today.AddDays(-1)
            };

            Assert.Throws<ArgumentException>(() => _repository.AddVehicle(invalidVehicle));
        }

        /// <summary>
        /// Tests that the <see cref="VehicleRepository.GetAllVehicles"/> method throws an <see cref="ArgumentException"/> 
        /// exception when the vehicle plate is invalid.
        /// </summary>
        [Fact]
        public void GetAllVehicles_ThrowsArgumentException_WhenInvalidPlate()
        {
            var invalidPlate = "1ABC234";

            Assert.Throws<ArgumentException>(() => _repository.GetAllVehicles(invalidPlate));
        }

        /// <summary>
        /// Tests that the <see cref="VehicleRepository.GetVehiclesByPlateAndEventType"/> method throws an <see cref="ArgumentException"/> 
        /// exception when the vehicle plate is invalid.
        /// </summary>
        [Fact]
        public void GetVehiclesByPlateAndEventType_ThrowsArgumentException_WhenInvalidPlate()
        {
            var invalidPlate = "1ABC234";

            Assert.Throws<ArgumentException>(() => _repository.GetVehiclesByPlateAndEventType(invalidPlate, EventTypeEnum.Rented));
        }

        /// <summary>
        /// Tests that the <see cref="VehicleRepository.GetVehiclesByPlateAndEventType"/> method throws an <see cref="ArgumentException"/> 
        /// exception when the event type is invalid.
        /// </summary>
        [Fact]
        public void GetVehiclesByPlateAndEventType_ThrowsArgumentException_WhenInvalidEventType()
        {
            var plate = "ABC1234";
            var invalidEventType = (EventTypeEnum)10;

            Assert.Throws<ArgumentException>(() => _repository.GetVehiclesByPlateAndEventType(plate, invalidEventType));
        }
    }
}

