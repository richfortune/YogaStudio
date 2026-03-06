using Microsoft.AspNetCore.Mvc;
using YogaStudio.Core.Entities;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IRepository<Booking> _repository;

    public BookingsController(IRepository<Booking> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Booking>>> Get()
    {
        var bookings = await _repository.ListAllAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> Get(Guid id)
    {
        var booking = await _repository.GetByIdAsync(id);
        if (booking == null) return NotFound();
        return Ok(booking);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> Post([FromBody] Booking booking)
    {
        booking.Id = Guid.NewGuid();
        await _repository.AddAsync(booking);
        return CreatedAtAction(nameof(Get), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Booking booking)
    {
        if (id != booking.Id) return BadRequest();
        await _repository.UpdateAsync(booking);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var booking = await _repository.GetByIdAsync(id);
        if (booking == null) return NotFound();
        await _repository.DeleteAsync(booking);
        return NoContent();
    }
}
