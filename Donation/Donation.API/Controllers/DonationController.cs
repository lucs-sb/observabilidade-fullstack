using Donation.API.Models;
using Donation.Domain.DTOs;
using Donation.Domain.DTOs.Response;
using Donation.Domain.Intefaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Donation.API.Controllers;

[ApiController]
[Route("/api/donation")]
public class DonationController : ControllerBase
{
    private readonly IDonationService _donationService;

    public DonationController(IDonationService donationService)
    {
        _donationService = donationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDonationAsync([FromBody] DonationModel donationModel)
    {
        DonationDTO donationDTO = donationModel.Adapt<DonationDTO>();
        await _donationService.CreateDonationAsync(donationDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonationAsync([FromRoute] string id)
    {
        await _donationService.DeleteDonationAsync(Guid.Parse(id));
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDonationsAsync()
    {
        List<DonationResponseDTO> donations = await _donationService.GetAllDonationsAsync();
        return Ok(donations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonationByIdAsync([FromRoute] string id)
    {
        DonationResponseDTO donation = await _donationService.GetDonationByIdAsync(Guid.Parse(id));
        return Ok(donation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonationAsync([FromRoute] string id, [FromBody] DonationModel donationModel)
    {
        DonationDTO donationDTO = donationModel.Adapt<DonationDTO>();
        await _donationService.UpdateDonationAsync(Guid.Parse(id), donationDTO);
        return Accepted();
    }
}