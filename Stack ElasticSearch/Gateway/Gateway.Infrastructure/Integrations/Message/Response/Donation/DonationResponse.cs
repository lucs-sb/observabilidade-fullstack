using Gateway.Domain.Enums;
using System.Text.Json.Serialization;

namespace Gateway.Infrastructure.Integrations.Message.Response.Donation;

public class DonationResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("donorId")]
    public Guid DonorId { get; set; }

    [JsonPropertyName("dateOfDonation")]
    public DateTime DateOfDonation { get; set; }

    [JsonPropertyName("donationType")]
    public DonationType DonationType { get; set; }

    [JsonPropertyName("volumeMl")]
    public int VolumeMl { get; set; }
    
    [JsonPropertyName("bagNumber")]
    public string? BagNumber { get; set; }
}