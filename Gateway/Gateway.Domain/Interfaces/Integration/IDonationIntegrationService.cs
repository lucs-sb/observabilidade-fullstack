using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;

namespace Gateway.Domain.Interfaces.Integration;

public interface IDonationIntegrationService
{
    Task CreateDonationAsync(DonationDTO donationDTO);
    Task<DonationResponseDTO> GetDonationByIdAsync(Guid id);
    Task<IEnumerable<List<DonationResponseDTO>>> GetAllDonationsAsync(Guid donorId);
    Task UpdateDonationAsync(Guid id, DonationDTO donationDTO);
    Task DeleteDonationAsync(Guid id);
}
