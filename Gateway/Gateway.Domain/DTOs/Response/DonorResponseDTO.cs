namespace Gateway.Domain.DTOs.Response;

public record DonorResponseDTO(
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    string Gender,
    string BloodType,
    AddressResponseDTO Address)
{
}
