using Donation.Application.Resources;
using Donation.Domain.DTOs;
using Donation.Domain.DTOs.Response;
using Donation.Domain.Intefaces;
using Donation.Domain.Intefaces.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Donation.Application.Services;

public class DonationService : IDonationService
{
    private readonly IUnitOfWork _unitOfWork;

    public DonationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateDonationAsync(DonationDTO donationDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Domain.Entities.Donation? donation = await _unitOfWork.Repository<Domain.Entities.Donation>().GetByBagNumberAsync(donationDTO.BagNumber);

            if (donation != null)
                throw new InvalidOperationException(BusinessMessage.Donation_DuplicateBagNumber_Warning);

            donation = donationDTO.Adapt<Domain.Entities.Donation>();

            await _unitOfWork.Repository<Domain.Entities.Donation>().AddAsync(donation);

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await _unitOfWork.RollbackAsync();

            throw new InvalidOperationException(BusinessMessage.Donation_DuplicateBagNumber_Warning);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task DeleteDonationAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Domain.Entities.Donation? donation = await _unitOfWork.Repository<Domain.Entities.Donation>().GetByIdAsync(id) ?? throw new InvalidOperationException(BusinessMessage.Donation_NotFound_Warning);

            _unitOfWork.Repository<Domain.Entities.Donation>().Remove(donation);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task<List<DonationResponseDTO>> GetAllDonationsAsync()
    {
        List<Domain.Entities.Donation> donations = await _unitOfWork.Repository<Domain.Entities.Donation>().GetAllAsync();

        return donations.Adapt<List<DonationResponseDTO>>();
    }

    public async Task<DonationResponseDTO> GetDonationByIdAsync(Guid id)
    {
        Domain.Entities.Donation donation = await _unitOfWork.Repository<Domain.Entities.Donation>().GetByIdAsync(id) ?? throw new InvalidOperationException(BusinessMessage.Donation_NotFound_Warning);

        return donation.Adapt<DonationResponseDTO>();
    }

    public async Task UpdateDonationAsync(Guid id, DonationDTO donationDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Domain.Entities.Donation? donation = await _unitOfWork.Repository<Domain.Entities.Donation>().GetByIdAsync(id) ?? throw new InvalidOperationException(BusinessMessage.Donation_NotFound_Warning);

            donationDTO.Adapt(donation);

            _unitOfWork.Repository<Domain.Entities.Donation>().Update(donation);

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await _unitOfWork.RollbackAsync();

            throw new InvalidOperationException(BusinessMessage.Donation_DuplicateBagNumber_Warning);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }
}