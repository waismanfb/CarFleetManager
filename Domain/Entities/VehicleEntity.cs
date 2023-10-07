using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class VehicleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public VehicleEntity() { }

        /// <summary>   
        /// 
        /// </summary>
        [Required]
        public string Plate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public VehicleModelEnum Model { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]

        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RentalEventEntity RentalEvents { get; set; }
    }
}
