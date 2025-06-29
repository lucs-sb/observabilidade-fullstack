namespace Gateway.Domain.DTOs;

public record DonorDTO (
    string FullName, 
    string Email,
    string PhoneNumber, 
    DateTime DateOfBirth, 
    string Gender,
    string BloodType, 
    AddressDTO Address) 
{
}
