using Donor.Application.Resources;
using Donor.Domain.DTOs;
using Donor.Domain.DTOs.Response;
using Donor.Domain.Intefaces;
using Donor.Domain.Intefaces.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Donor.Application.Services;

public class DonorService : IDonorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DonorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateDonorAsync(DonorDTO donorDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Domain.Entities.Donor? donor = await _unitOfWork.Repository<Domain.Entities.Donor>().GetByEmailAsync(donorDTO.Email);

            if (donor != null)
                throw new InvalidOperationException(BusinessMessage.Donor_DuplicateEmail_Warning);

            donor = donorDTO.Adapt<Domain.Entities.Donor>();

            await _unitOfWork.Repository<Domain.Entities.Donor>().AddAsync(donor);

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await _unitOfWork.RollbackAsync();

            throw new InvalidOperationException(BusinessMessage.Donor_DuplicateEmail_Warning);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task DeleteDonorAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Domain.Entities.Donor? donor = await _unitOfWork.Repository<Domain.Entities.Donor>().GetByIdAsync(id) ?? throw new InvalidOperationException(BusinessMessage.Donor_NotFound_Warning);

            _unitOfWork.Repository<Domain.Entities.Donor>().Remove(donor);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task<List<DonorResponseDTO>> GetAllDonorsAsync()
    {
        List<Domain.Entities.Donor> donors = await _unitOfWork.Repository<Domain.Entities.Donor>().GetAllAsync();

        return donors.Adapt<List<DonorResponseDTO>>();
    }

    public async Task<DonorResponseDTO> GetDonorByIdAsync(Guid id)
    {
        Domain.Entities.Donor donor = await _unitOfWork.Repository<Domain.Entities.Donor>().GetByIdAsync(id) ?? throw new InvalidOperationException(BusinessMessage.Donor_NotFound_Warning);

        return donor.Adapt<DonorResponseDTO>();
    }

    public async Task UpdateDonorAsync(Guid id, DonorDTO donorDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Domain.Entities.Donor? donor = await _unitOfWork.Repository<Domain.Entities.Donor>().GetByIdAsync(id) ?? throw new InvalidOperationException(BusinessMessage.Donor_NotFound_Warning);

            donorDTO.Adapt(donor);

            _unitOfWork.Repository<Domain.Entities.Donor>().Update(donor);

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await _unitOfWork.RollbackAsync();

            throw new InvalidOperationException(BusinessMessage.Donor_DuplicateEmail_Warning);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }
}