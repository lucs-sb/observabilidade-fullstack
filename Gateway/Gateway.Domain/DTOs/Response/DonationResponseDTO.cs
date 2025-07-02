using Gateway.Domain.Enums;

namespace Gateway.Domain.DTOs.Response;

public record DonationResponseDTO(
    Guid Id,
    Guid DonorId,
    DateTime DateOfDonation,
    DonationType DonationType,
    int VolumeMl,
    string BagNumber)
{
}
