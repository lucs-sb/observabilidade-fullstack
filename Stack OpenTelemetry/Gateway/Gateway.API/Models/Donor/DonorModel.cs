using Gateway.API.Models.Donor.Classes;
using Gateway.Domain.Enums;

namespace Gateway.API.Models.Donor;

public class DonorModel
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public BloodType? BloodType { get; set; }
    public RhFactor? RhFactor { get; set; }
    public decimal? WeightKg { get; set; }
    public AddressModel? Address { get; set; }
}
