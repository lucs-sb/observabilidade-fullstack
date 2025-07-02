using Gateway.API.Models.Donation;
using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Infrastructure.Integrations.Message.Request.Donation;
using Gateway.Infrastructure.Integrations.Message.Response.Donation;
using Mapster;

namespace Gateway.API.Mappers;

public static class DonationMappingConfigurations
{
    public static void RegisterDonationMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<DonationModel, DonationDTO>
            .NewConfig()
            .Map(dest => dest.DonorId, src => src.DonorId)
            .Map(dest => dest.DateOfDonation, src => src.DateOfDonation)
            .Map(dest => dest.DonationType, src => src.DonationType)
            .Map(dest => dest.VolumeMl, src => src.VolumeMl)
            .Map(dest => dest.BagNumber, src => src.BagNumber);

        TypeAdapterConfig<DonationDTO, DonationRequest>
            .NewConfig()
            .Map(dest => dest.DonorId, src => src.DonorId)
            .Map(dest => dest.DateOfDonation, src => src.DateOfDonation)
            .Map(dest => dest.DonationType, src => src.DonationType)
            .Map(dest => dest.VolumeMl, src => src.VolumeMl)
            .Map(dest => dest.BagNumber, src => src.BagNumber);

        TypeAdapterConfig<DonationResponse, DonationResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.DonorId, src => src.DonorId)
            .Map(dest => dest.DateOfDonation, src => src.DateOfDonation)
            .Map(dest => dest.DonationType, src => src.DonationType)
            .Map(dest => dest.VolumeMl, src => src.VolumeMl)
            .Map(dest => dest.BagNumber, src => src.BagNumber);
    }
}
