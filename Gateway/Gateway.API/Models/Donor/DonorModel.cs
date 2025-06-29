using Gateway.API.Models.Donor.Classes;

namespace Gateway.API.Models.Donor;

public class DonorModel
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? BloodType { get; set; }
    public AddressModel? Address { get; set; }
}
