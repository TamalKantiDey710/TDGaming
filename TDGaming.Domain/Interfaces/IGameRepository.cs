using TDGaming.API.Common;
using TDGaming.Domain.Entities;

namespace TDGaming.Domain.Interfaces;

public interface IGameRepository
{
    Task<IEnumerable<VideoGame>> GetAllAsync();
    Task<PagedResult<VideoGame>> GetAllAsync(string? Title, int PageNumber, int PageSize);
    Task<VideoGame?> GetByIdAsync(Guid id);
    Task AddAsync(VideoGame game);
    Task UpdateAsync(VideoGame game);
}
