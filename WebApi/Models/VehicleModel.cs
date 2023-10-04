using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsRented { get; set; }
        public List<RentalEventEntity>? RentalEvents { get; set; }
    }
}
