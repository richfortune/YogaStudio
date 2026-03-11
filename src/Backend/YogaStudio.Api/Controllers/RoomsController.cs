using Microsoft.AspNetCore.Mvc;
using YogaStudio.Core.DTOs;
using YogaStudio.Core.Entities;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRepository<Room> _repository;

    public RoomsController(IRepository<Room> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RoomDto>>> Get()
    {
        var rooms = await _repository.ListAllAsync();
        var dtos = rooms.Select(r => MapToDto(r)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> Get(Guid id)
    {
        var room = await _repository.GetByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(MapToDto(room));
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Post([FromBody] CreateRoomDto dto)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Capacity = dto.Capacity,
            Address = dto.Address
        };
        await _repository.AddAsync(room);
        return CreatedAtAction(nameof(Get), new { id = room.Id }, MapToDto(room));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UpdateRoomDto dto)
    {
        var room = await _repository.GetByIdAsync(id);
        if (room == null) return NotFound();

        room.Name = dto.Name;
        room.Capacity = dto.Capacity;
        room.Address = dto.Address;

        await _repository.UpdateAsync(room);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var room = await _repository.GetByIdAsync(id);
        if (room == null) return NotFound();
        await _repository.DeleteAsync(room);
        return NoContent();
    }

    private static RoomDto MapToDto(Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Capacity = room.Capacity,
            Address = room.Address
        };
    }
}
