using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Domain.Interfaces;
using Gateway.Domain.Interfaces.Integration;

namespace Gateway.Application.Services;

public class DonationService : IDonationService
{
    private readonly IDonationIntegrationService _donationIntegrationService;

    public DonationService(IDonationIntegrationService donationIntegrationService)
    {
        _donationIntegrationService = donationIntegrationService;
    }

    public async Task CreateDonationAsync(DonationDTO donationDTO)
    {
        await _donationIntegrationService.CreateDonationAsync(donationDTO);
    }

    public async Task DeleteDonationAsync(Guid id)
    {
        await _donationIntegrationService.DeleteDonationAsync(id);
    }

    public async Task<List<DonationResponseDTO>> GetAllDonationsAsync(Guid donorId)
    {
        return await _donationIntegrationService.GetAllDonationsAsync(donorId);
    }

    public async Task<DonationResponseDTO> GetDonationByIdAsync(Guid id)
    {
        return await _donationIntegrationService.GetDonationByIdAsync(id);
    }

    public async Task UpdateDonationAsync(Guid id, DonationDTO donationDTO)
    {
        await _donationIntegrationService.UpdateDonationAsync(id, donationDTO);
    }
}