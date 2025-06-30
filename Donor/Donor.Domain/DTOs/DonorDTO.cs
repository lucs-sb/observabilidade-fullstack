using Donor.Domain.Enums;

namespace Donor.Domain.DTOs;

public record DonorDTO(
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    Gender Gender,
    BloodType BloodType,
    RhFactor RhFactor,
    decimal WeightKg,
    AddressDTO Address)
{
}