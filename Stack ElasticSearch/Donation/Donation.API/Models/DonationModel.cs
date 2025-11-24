using Donation.Domain.Enums;

namespace Donation.API.Models;

public class DonationModel
{
    public Guid DonorId { get; set; }
    public DateTime DateOfDonation { get; set; }
    public DonationType DonationType { get; set; }
    public int VolumeMl { get; set; }
    public string? BagNumber { get; set; }
}