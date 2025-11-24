using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;

namespace Gateway.Domain.Interfaces;

public interface IDonorService
{
    Task CreateDonorAsync(DonorDTO donorDTO);
    Task<DonorResponseDTO> GetDonorByIdAsync(Guid id);
    Task<List<DonorResponseDTO>> GetAllDonorsAsync();
    Task UpdateDonorAsync(Guid id, DonorDTO donorDTO);
    Task DeleteDonorAsync(Guid id);
}
