using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a rental event entity.
    /// </summary>
    public class RentalEventEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentalEventEntity"/> class.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        public RentalEventEntity(VehicleEntity vehicle)
        {
            VehiclePlate = vehicle.Plate;
            EventType = vehicle.RentalEvents.EventType;
            EventDate = vehicle.RentalEvents.EventDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalEventEntity"/> class.
        /// </summary>
        public RentalEventEntity() { }

        /// <summary>
        /// Gets or sets the vehicle plate.
        /// </summary>
        public string? VehiclePlate { get; private set; }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        [Required]
        public EventTypeEnum EventType { get; set; }

        /// <summary>
        /// Gets or sets the date of the event.
        /// </summary>
        [Required]
        public DateTime EventDate { get; set; }
    }
}
