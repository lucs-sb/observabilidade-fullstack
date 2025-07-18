﻿using Gateway.API.Models.Donation;
using Gateway.API.Resources;
using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiController]
[Route("/api/donation")]
[Authorize(Roles = "Admin")]
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
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonationAsync([FromRoute] string id)
    {
        if (!Guid.TryParse(id, out _))
            throw new InvalidOperationException(ApiMessage.Gateway_InvalidId_Warning);

        await _donationService.DeleteDonationAsync(Guid.Parse(id));
        return NoContent();
    }

    [HttpGet("donor/{donorId}")]
    public async Task<IActionResult> GetAllDonationsAsync([FromRoute] string donorId)
    {
        if (!Guid.TryParse(donorId, out _))
            throw new InvalidOperationException(ApiMessage.Gateway_InvalidId_Warning);

        List<DonationResponseDTO> donations = await _donationService.GetAllDonationsAsync(Guid.Parse(donorId));
        return Ok(donations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonationByIdAsync([FromRoute] string id)
    {
        if (!Guid.TryParse(id, out _))
            throw new InvalidOperationException(ApiMessage.Gateway_InvalidId_Warning);

        DonationResponseDTO donation = await _donationService.GetDonationByIdAsync(Guid.Parse(id));
        return Ok(donation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonationAsync([FromRoute] string id, [FromBody] DonationModel donationModel)
    {
        if (!Guid.TryParse(id, out _))
            throw new InvalidOperationException(ApiMessage.Gateway_InvalidId_Warning);

        DonationDTO donationDTO = donationModel.Adapt<DonationDTO>();
        await _donationService.UpdateDonationAsync(Guid.Parse(id), donationDTO);
        return Accepted();
    }
}
