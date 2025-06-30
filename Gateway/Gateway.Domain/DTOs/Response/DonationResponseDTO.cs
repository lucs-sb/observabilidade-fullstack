using Gateway.Domain.Enums;

namespace Gateway.Domain.DTOs.Response;

public record DonationResponseDTO(
    Guid DonorId,
    DateTime DateOfDonation,
    DonationType DonationType,
    int VolumeMl,
    string BagNumber)
{
}
