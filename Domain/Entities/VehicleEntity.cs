using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a vehicle entity.
    /// </summary>
    public class VehicleEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleEntity"/> class.
        /// </summary>
        public VehicleEntity() { }

        /// <summary>
        /// Gets or sets the vehicle plate.
        /// </summary>
        [Required]
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets the vehicle model.
        /// </summary>
        [Required]
        public VehicleModelEnum Model { get; set; }

        /// <summary>
        /// Gets or sets the vehicle registration date.
        /// </summary>
        [Required]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the rental events of the vehicle.
        /// </summary>
        public RentalEventEntity RentalEvents { get; set; }
    }
}
