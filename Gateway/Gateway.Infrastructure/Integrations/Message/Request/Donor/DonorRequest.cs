﻿namespace Gateway.Infrastructure.Integrations.Message.Request.Donor;

public class DonorRequest
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? BloodType { get; set; }
    public string? RhFactor { get; set; }
    public decimal? WeightKg { get; set; }
    public AddressRequest? Address { get; set; }
}
