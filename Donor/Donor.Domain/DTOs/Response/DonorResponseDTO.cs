namespace Donor.Domain.DTOs.Response;

public record DonorResponseDTO(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    int Gender,
    int BloodType,
    int RhFactor,
    decimal WeightKg,
    AddressResponseDTO Address)
{
}