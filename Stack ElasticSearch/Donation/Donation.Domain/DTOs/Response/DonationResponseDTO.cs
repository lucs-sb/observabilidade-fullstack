namespace Donation.Domain.DTOs.Response;

public record DonationResponseDTO(
    Guid Id,
    Guid DonorId,
    DateTime DateOfDonation,
    int DonationType,
    int VolumeMl,
    string BagNumber)
{
}