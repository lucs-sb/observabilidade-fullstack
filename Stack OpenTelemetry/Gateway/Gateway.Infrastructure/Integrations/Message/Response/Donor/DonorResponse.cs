using Gateway.Domain.Enums;
using System.Text.Json.Serialization;

namespace Gateway.Infrastructure.Integrations.Message.Response.Donor;

public class DonorResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [JsonPropertyName("gender")]
    public Gender Gender { get; set; }

    [JsonPropertyName("bloodType")]
    public BloodType BloodType { get; set; }

    [JsonPropertyName("rhFactor")]
    public RhFactor RhFactor { get; set; }

    [JsonPropertyName("weightKg")]
    public decimal? WeightKg { get; set; }

    [JsonPropertyName("address")]
    public AddressResponse? Address { get; set; }
}
