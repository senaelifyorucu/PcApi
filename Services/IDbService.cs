using PcApi.DTOs;

namespace PcApi.Services;

public interface IDbService
{
    Task<IEnumerable<GetPcDto>> GetAllAsync();

    Task<GetPcDetailsDto> GetByIdAsync(int id);

    Task<GetPcDto> CreateAsync(CreatePcDto dto);

    Task UpdateAsync(int id, UpdatePcDto dto);

    Task DeleteAsync(int id);
}