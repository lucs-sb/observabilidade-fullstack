using Gateway.Domain.Enums;

namespace Gateway.Domain.DTOs;

public record DonationDTO(
    Guid DonorId,
    DateTime DateOfDonation,
    DonationType DonationType,
    int VolumeMl,
    string BagNumber)
{
}