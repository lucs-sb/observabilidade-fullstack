using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Domain.Interfaces;
using Gateway.Domain.Interfaces.Integration;

namespace Gateway.Application.Services;

public class DonorService : IDonorService
{
    private readonly IDonorIntegrationService _donorIntegrationService;

    public DonorService(IDonorIntegrationService donorIntegrationService)
    {
        _donorIntegrationService = donorIntegrationService;
    }

    public async Task CreateDonorAsync(DonorDTO donorDTO)
    {
        await _donorIntegrationService.CreateDonorAsync(donorDTO);
    }

    public async Task DeleteDonorAsync(Guid id)
    {
        await _donorIntegrationService.DeleteDonorAsync(id);
    }

    public async Task<List<DonorResponseDTO>> GetAllDonorsAsync()
    {
        return await _donorIntegrationService.GetAllDonorsAsync();
    }

    public async Task<DonorResponseDTO> GetDonorByIdAsync(Guid id)
    {
        return await _donorIntegrationService.GetDonorByIdAsync(id);
    }

    public async Task UpdateDonorAsync(Guid id, DonorDTO donorDTO)
    {
        await _donorIntegrationService.UpdateDonorAsync(id, donorDTO);
    }
}