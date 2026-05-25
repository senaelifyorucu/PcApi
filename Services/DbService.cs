using Microsoft.EntityFrameworkCore;
using PcApi.Data;
using PcApi.DTOs;
using PcApi.Exceptions;
using PcApi.Models;

namespace PcApi.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _context;

    public DbService(AppDbContext context)
    {
        _context = context;
    }

    // GET ALL
    public async Task<IEnumerable<GetPcDto>> GetAllAsync()
    {
        return await _context.PCs
            .Select(pc => new GetPcDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }


    // GET BY ID
    public async Task<GetPcDetailsDto> GetByIdAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.Manufacturer)

            .Include(p => p.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.Type)

            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            throw new NotFoundException($"PC {id} not found");

        return new GetPcDetailsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,

            Components = pc.PCComponents.Select(x =>
                new GetPcComponentDto
                {
                    Amount = x.Amount,

                    Component = new GetComponentDto
                    {
                        Code = x.Component.Code,
                        Name = x.Component.Name,
                        Description = x.Component.Description,

                        Manufacturer = new GetManufacturerDto
                        {
                            Id = x.Component.Manufacturer.Id,
                            Abbreviation = x.Component.Manufacturer.Abbreviation,
                            FullName = x.Component.Manufacturer.FullName,
                            FoundationDate = x.Component.Manufacturer.FoundationDate
                        },

                        Type = new GetTypeDto
                        {
                            Id = x.Component.Type.Id,
                            Abbreviation = x.Component.Type.Abbreviation,
                            Name = x.Component.Type.Name
                        }
                    }
                })
                .ToList()
        };
    }


    // POST
    public async Task<GetPcDto> CreateAsync(CreatePcDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);

        await _context.SaveChangesAsync();

        return new GetPcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }


    // PUT
    public async Task UpdateAsync(int id, UpdatePcDto dto)
    {
        var pc = await _context.PCs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pc == null)
            throw new NotFoundException($"PC {id} not found");

        if (dto.Name != null)
            pc.Name = dto.Name;

        if (dto.Weight.HasValue)
            pc.Weight = dto.Weight.Value;

        if (dto.Warranty.HasValue)
            pc.Warranty = dto.Warranty.Value;

        if (dto.CreatedAt.HasValue)
            pc.CreatedAt = dto.CreatedAt.Value;

        if (dto.Stock.HasValue)
            pc.Stock = dto.Stock.Value;

        await _context.SaveChangesAsync();
    }


    // DELETE
    public async Task DeleteAsync(int id)
    {
        var pc = await _context.PCs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pc == null)
            throw new NotFoundException($"PC {id} not found");

        _context.PCs.Remove(pc);

        await _context.SaveChangesAsync();
    }
}