using Gateway.Domain.Enums;

namespace Gateway.Infrastructure.Integrations.Message.Response.Donation;

public class DonationResponse
{
    public Guid DonorId { get; set; }
    public DateTime DateOfDonation { get; set; }
    public DonationType DonationType { get; set; }
    public int VolumeMl { get; set; }
    public string? BagNumber { get; set; }
}