using Microsoft.EntityFrameworkCore;
using TDGaming.API.Common;
using TDGaming.Domain.Entities;
using TDGaming.Domain.Interfaces;
using TDGaming.Infrastructure.Data;

namespace TDGaming.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<VideoGame>> GetAllAsync() =>
        await _context.Games.OrderBy(o=>o.Title).ToListAsync();

    public async Task<PagedResult<VideoGame>> GetAllAsync(string? Title, int PageNumber, int PageSize)
    {
        var query =  _context.Games.OrderBy(o => o.Title).AsQueryable();
        if (!string.IsNullOrWhiteSpace(Title))
        {
            query = query.Where(v => v.Title.Contains(Title));
        }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            var result = new PagedResult<VideoGame>
            {
                Items = items,
                TotalNumber = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize
            };

        return (PagedResult<VideoGame>)result;
    }


    public async Task<VideoGame?> GetByIdAsync(Guid id) =>
        await _context.Games.FindAsync(id);

    public async Task AddAsync(VideoGame game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(VideoGame game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(VideoGame game)
    {
         _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }
}
