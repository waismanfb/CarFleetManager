using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RentalEventEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public RentalEventEntity(VehicleEntity vehicle)
        {
            VehiclePlate = vehicle.Plate;
            EventType = vehicle.RentalEvents.EventType;
            EventDate = vehicle.RentalEvents.EventDate;
        }

        /// <summary>
        /// 
        /// </summary>
        public RentalEventEntity() { }

        /// <summary>
        /// 
        /// </summary>
        public string? VehiclePlate { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public EventTypeEnum EventType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime EventDate { get; set; }

    }
}
