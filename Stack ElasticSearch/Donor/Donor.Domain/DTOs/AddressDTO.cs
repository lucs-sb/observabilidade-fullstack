namespace Donor.Domain.DTOs;

public record AddressDTO(string Street,
    string City,
    string State,
    string ZipCode)
{
}