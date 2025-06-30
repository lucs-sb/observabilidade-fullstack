using Gateway.Domain.Enums;

namespace Gateway.Infrastructure.Integrations.Message.Request.Donation;

public class DonationRequest
{
    public Guid DonorId { get; set; }
    public DateTime DateOfDonation { get; set; }
    public string? DonationType { get; set; }
    public int VolumeMl { get; set; }
    public string? BagNumber { get; set; }
}
