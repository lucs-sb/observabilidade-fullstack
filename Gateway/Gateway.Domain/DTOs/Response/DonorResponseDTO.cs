namespace Gateway.Domain.DTOs.Response;

public record DonorResponseDTO(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    string Gender,
    string BloodType,
    string RhFactor,
    decimal WeightKg,
    AddressResponseDTO Address)
{
}
