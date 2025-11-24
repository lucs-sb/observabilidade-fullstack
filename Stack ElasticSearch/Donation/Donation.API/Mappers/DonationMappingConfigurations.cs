using Donation.API.Models;
using Donation.Domain.DTOs;
using Donation.Domain.DTOs.Response;
using Mapster;

namespace Donation.API.Mappers;

public static class DonationMappingConfigurations
{
    public static IServiceCollection RegisterDonationMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<DonationModel, DonationDTO>
            .NewConfig()
            .Map(dest => dest.DonorId, src => src.DonorId)
            .Map(dest => dest.DateOfDonation, src => src.DateOfDonation)
            .Map(dest => dest.DonationType, src => src.DonationType)
            .Map(dest => dest.VolumeMl, src => src.VolumeMl)
            .Map(dest => dest.BagNumber, src => src.BagNumber);

        TypeAdapterConfig<DonationDTO, Domain.Entities.Donation>
            .NewConfig()
            .Map(dest => dest.DonorId, src => src.DonorId)
            .Map(dest => dest.DateOfDonation, src => src.DateOfDonation.ToUniversalTime())
            .Map(dest => dest.DonationType, src => src.DonationType)
            .Map(dest => dest.VolumeMl, src => src.VolumeMl)
            .Map(dest => dest.BagNumber, src => src.BagNumber);

        TypeAdapterConfig<Domain.Entities.Donation, DonationResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.DonorId, src => src.DonorId)
            .Map(dest => dest.DateOfDonation, src => src.DateOfDonation)
            .Map(dest => dest.DonationType, src => src.DonationType)
            .Map(dest => dest.VolumeMl, src => src.VolumeMl)
            .Map(dest => dest.BagNumber, src => src.BagNumber);

        return services;
    }
}