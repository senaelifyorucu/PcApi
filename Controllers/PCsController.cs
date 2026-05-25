using Microsoft.AspNetCore.Mvc;
using PcApi.DTOs;
using PcApi.Exceptions;
using PcApi.Services;

namespace PcApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PCsController(IDbService dbService)
    {
        _dbService = dbService;
    }


    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _dbService.GetAllAsync();

        return Ok(result);
    }


    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _dbService.GetByIdAsync(id);

            return Ok(result);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }


    // POST
    [HttpPost]
    public async Task<IActionResult> Create(CreatePcDto dto)
    {
        var result = await _dbService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result
        );
    }


    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePcDto dto)
    {
        try
        {
            await _dbService.UpdateAsync(id, dto);

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }


    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _dbService.DeleteAsync(id);

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}