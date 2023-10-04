using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VehicleEntity
    {
        public VehicleEntity(string plate, VehicleTypeEnum model, DateTime registrationDate, bool isRented)
        {
            Plate = plate;
            Model = model;
            RegistrationDate = registrationDate;
            IsRented = isRented;
        }

        public int Id { get; private set; }
        [Required]
        public string Plate { get; set; }
        [Required]
        public VehicleTypeEnum Model { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public bool IsRented { get; set; }
        public List<RentalEventEntity>? RentalEvents { get; set; }
    }
}
