using Donation.Domain.DTOs;
using Donation.Domain.DTOs.Response;

namespace Donation.Domain.Intefaces;

public interface IDonationService
{
    Task CreateDonationAsync(DonationDTO donationDTO);
    Task<DonationResponseDTO> GetDonationByIdAsync(Guid id);
    Task<List<DonationResponseDTO>> GetAllDonationsAsync();
    Task UpdateDonationAsync(Guid id, DonationDTO donationDTO);
    Task DeleteDonationAsync(Guid id);
}