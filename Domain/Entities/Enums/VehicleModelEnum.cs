using System.ComponentModel.DataAnnotations;

public enum VehicleModelEnum
{
    /// <summary>
    /// Vehicle model hatchback
    /// </summary>
    [Display(Name = "Hatchback")]
    Hatchback = 11,

    /// <summary>
    /// Vehicle model sedan
    /// </summary>
    [Display(Name = "Sedan")]
    Sedan = 22,

    /// <summary>
    ///  Vehicle model SUV
    /// </summary>
    [Display(Name = "SUV")]
    SUV = 33
}