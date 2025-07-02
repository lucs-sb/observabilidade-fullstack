using Donor.API.Models;
using Donor.Domain.DTOs;
using Donor.Domain.DTOs.Response;
using Mapster;

namespace Donor.API.Mappers;

public static class DonorMappingConfigurations
{
    public static IServiceCollection RegisterDonorMaps(this IServiceCollection services)
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

        TypeAdapterConfig<Domain.Entities.Donor, DonorResponseDTO>
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
            .Map(dest => dest.Address, src => src.Adapt<AddressDTO>());

        TypeAdapterConfig<Domain.Entities.Donor, AddressResponseDTO>
            .NewConfig()
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode);

        return services;
    }
}