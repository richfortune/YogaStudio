using Microsoft.AspNetCore.Mvc;
using YogaStudio.Core.DTOs;
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
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> Get()
    {
        var bookings = await _repository.ListAllAsync();
        var dtos = bookings.Select(b => MapToDto(b)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingDto>> Get(Guid id)
    {
        var booking = await _repository.GetByIdAsync(id);
        if (booking == null) return NotFound();
        return Ok(MapToDto(booking));
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> Post([FromBody] CreateBookingDto dto)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            SessionId = dto.SessionId,
            StudentUserId = dto.StudentUserId,
            Status = dto.Status
        };
        await _repository.AddAsync(booking);
        return CreatedAtAction(nameof(Get), new { id = booking.Id }, MapToDto(booking));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UpdateBookingDto dto)
    {
        var booking = await _repository.GetByIdAsync(id);
        if (booking == null) return NotFound();

        booking.SessionId = dto.SessionId;
        booking.StudentUserId = dto.StudentUserId;
        booking.Status = dto.Status;

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

    private static BookingDto MapToDto(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            SessionId = booking.SessionId,
            StudentUserId = booking.StudentUserId,
            Status = booking.Status
        };
    }
}
