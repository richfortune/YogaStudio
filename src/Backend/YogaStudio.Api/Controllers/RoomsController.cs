using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IReadOnlyList<Room>>> Get()
    {
        var rooms = await _repository.ListAllAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> Get(Guid id)
    {
        var room = await _repository.GetByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> Post([FromBody] Room room)
    {
        room.Id = Guid.NewGuid();
        await _repository.AddAsync(room);
        return CreatedAtAction(nameof(Get), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Room room)
    {
        if (id != room.Id) return BadRequest();
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
}
