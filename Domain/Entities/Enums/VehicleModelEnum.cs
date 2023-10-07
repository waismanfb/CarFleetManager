using System.ComponentModel.DataAnnotations;

public enum VehicleModelEnum
{
    [Display(Name = "Hatchback")]
    Hatchback = 11,

    [Display(Name = "Sedan")]
    Sedan = 22,

    [Display(Name = "SUV")]
    SUV = 33
}