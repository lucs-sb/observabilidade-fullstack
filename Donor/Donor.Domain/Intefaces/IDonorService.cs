using Donor.Domain.DTOs;
using Donor.Domain.DTOs.Response;

namespace Donor.Domain.Intefaces;

public interface IDonorService
{
    Task CreateDonorAsync(DonorDTO donorDTO);
    Task<DonorResponseDTO> GetDonorByIdAsync(Guid id);
    Task<List<DonorResponseDTO>> GetAllDonorsAsync();
    Task UpdateDonorAsync(Guid id, DonorDTO donorDTO);
    Task DeleteDonorAsync(Guid id);
}