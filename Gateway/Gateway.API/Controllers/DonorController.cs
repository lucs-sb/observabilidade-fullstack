using Gateway.API.Models.Donor;
using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiController]
[Route("/api/donor")]
[Authorize(Roles = "Admin")]
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
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonorAsync([FromRoute] string id)
    {
        if (!Guid.TryParse(id, out _))
            return BadRequest("Invalid donor ID.");

        await _donorService.DeleteDonorAsync(Guid.Parse(id));
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDonorsAsync()
    {
        IEnumerable<List<DonorResponseDTO>> donors = await _donorService.GetAllDonorsAsync();
        return Ok(donors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonorByIdAsync([FromRoute] string id)
    {
        if (!Guid.TryParse(id, out _))
            return BadRequest("Invalid donor ID.");

        DonorResponseDTO donor = await _donorService.GetDonorByIdAsync(Guid.Parse(id));
        return Ok(donor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonorAsync([FromRoute] string id, [FromBody] DonorModel donorModel)
    {
        if (!Guid.TryParse(id, out _))
            return BadRequest("Invalid donor ID.");

        DonorDTO donorDTO = donorModel.Adapt<DonorDTO>();
        await _donorService.UpdateDonorAsync(Guid.Parse(id), donorDTO);
        return Accepted();
    }
}
