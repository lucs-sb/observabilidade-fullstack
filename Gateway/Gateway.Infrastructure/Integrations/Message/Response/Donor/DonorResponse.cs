using Gateway.Domain.Enums;

namespace Gateway.Infrastructure.Integrations.Message.Response.Donor;

public class DonorResponse
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public BloodType BloodType { get; set; }
    public RhFactor RhFactor { get; set; }
    public decimal? WeightKg { get; set; }
    public AddressResponse? Address { get; set; }
}
