using Gateway.API.Models.Donor;
using Gateway.API.Models.Donor.Classes;
using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Infrastructure.Integrations.Message.Request.Donor;
using Gateway.Infrastructure.Integrations.Message.Response.Donor;
using Mapster;

namespace Gateway.API.Mappers;

public static class DonorMappingConfigurations
{
    public static void RegisterDonorMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<DonorModel, DonorDTO>
            .NewConfig()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.BloodType, src => src.BloodType)
            .Map(dest => dest.RhFactor, src => src.RhFactor)
            .Map(dest => dest.WeightKg, src => src.WeightKg)
            .Map(dest => dest.Address, src => src.Address.Adapt<AddressDTO>());

        TypeAdapterConfig<AddressModel, AddressDTO>
            .NewConfig()
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode);

        TypeAdapterConfig<DonorDTO, DonorRequest>
            .NewConfig()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.BloodType, src => src.BloodType)
            .Map(dest => dest.RhFactor, src => src.RhFactor)
            .Map(dest => dest.WeightKg, src => src.WeightKg)
            .Map(dest => dest.Address, src => src.Address.Adapt<AddressDTO>());

        TypeAdapterConfig<AddressDTO, AddressRequest>
            .NewConfig()
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode);

        TypeAdapterConfig<DonorResponse, DonorResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.BloodType, src => src.BloodType)
            .Map(dest => dest.RhFactor, src => src.RhFactor)
            .Map(dest => dest.WeightKg, src => src.WeightKg)
            .Map(dest => dest.Address, src => src.Address.Adapt<AddressDTO>());

        TypeAdapterConfig<AddressResponse, AddressResponseDTO>
            .NewConfig()
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode);
    }
}
