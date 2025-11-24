using Donation.Domain.Enums;

namespace Donation.Domain.DTOs;

public record DonationDTO(
    Guid DonorId,
    DateTime DateOfDonation,
    DonationType DonationType,
    int VolumeMl,
    string BagNumber)
{
}