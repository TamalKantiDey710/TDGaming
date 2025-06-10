using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TDGaming.API.DTO;
using TDGaming.Domain.Entities;
using TDGaming.Domain.Interfaces;

namespace TDGaming.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesController : ControllerBase
{
    private readonly IGameRepository _repo;

    public VideoGamesController(IGameRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var games = await _repo.GetAllAsync();
        return Ok(games);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAll([FromQuery]VideoGameFilterDto filter)
    {
        var games = await _repo.GetAllAsync(filter.Name, filter.PageNumber, filter.PageSize);
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var game = await _repo.GetByIdAsync(id);

        return game is null ? NotFound() : Ok(game);
    }

    [HttpPost]
    public async Task<IActionResult> Create(VideoGameDto dto)
    {
        var game = new VideoGame(dto.Title, dto.Price);

        await _repo.AddAsync(game);

        return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, VideoGameDto dto)
    {
        var game = await _repo.GetByIdAsync(id);

        if (game is null) return NotFound();

        game.Update(dto.Title, dto.Price);

        await _repo.UpdateAsync(game);

        return NoContent();
    }
}
