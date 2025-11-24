namespace Donor.Domain.DTOs.Response;

public record AddressResponseDTO(
    string Street,
    string City,
    string State,
    string ZipCode)
{
}