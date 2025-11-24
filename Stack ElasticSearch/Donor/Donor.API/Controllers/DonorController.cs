using Donor.API.Models;
using Donor.Domain.DTOs;
using Donor.Domain.DTOs.Response;
using Donor.Domain.Intefaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Donor.API.Controllers;

[ApiController]
[Route("/api/donor")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _donorService;

    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDonorAsync([FromBody] DonorModel donorModel)
    {
        DonorDTO donorDTO = donorModel.Adapt<DonorDTO>();
        await _donorService.CreateDonorAsync(donorDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonorAsync([FromRoute] string id)
    {
        await _donorService.DeleteDonorAsync(Guid.Parse(id));
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDonorsAsync()
    {
        List<DonorResponseDTO> donors = await _donorService.GetAllDonorsAsync();
        return Ok(donors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonorByIdAsync([FromRoute] string id)
    {
        DonorResponseDTO donor = await _donorService.GetDonorByIdAsync(Guid.Parse(id));
        return Ok(donor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonorAsync([FromRoute] string id, [FromBody] DonorModel donorModel)
    {
        DonorDTO donorDTO = donorModel.Adapt<DonorDTO>();
        await _donorService.UpdateDonorAsync(Guid.Parse(id), donorDTO);
        return Accepted();
    }
}