using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;

namespace Gateway.Domain.Interfaces.Integration;

public interface IDonorIntegrationService
{
    Task CreateDonorAsync(DonorDTO donorDTO);
    Task<DonorResponseDTO> GetDonorByIdAsync(Guid id);
    Task<IEnumerable<List<DonorResponseDTO>>> GetAllDonorsAsync();
    Task UpdateDonorAsync(Guid id, DonorDTO donorDTO);
    Task DeleteDonorAsync(Guid id);
}
