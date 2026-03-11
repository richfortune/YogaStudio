using Microsoft.AspNetCore.Mvc;
using YogaStudio.Core.DTOs;
using YogaStudio.Core.Entities;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassSessionsController : ControllerBase
{
    private readonly IRepository<ClassSession> _repository;

    public ClassSessionsController(IRepository<ClassSession> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClassSessionDto>>> Get()
    {
        var sessions = await _repository.ListAllAsync();
        var dtos = sessions.Select(s => MapToDto(s)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassSessionDto>> Get(Guid id)
    {
        var session = await _repository.GetByIdAsync(id);
        if (session == null) return NotFound();
        return Ok(MapToDto(session));
    }

    [HttpPost]
    public async Task<ActionResult<ClassSessionDto>> Post([FromBody] CreateClassSessionDto dto)
    {
        var session = new ClassSession
        {
            Id = Guid.NewGuid(),
            YogaClassId = dto.YogaClassId,
            RoomId = dto.RoomId,
            InstructorUserId = dto.InstructorUserId,
            StartTime = dto.StartTime,
            DurationMinutes = dto.DurationMinutes,
            MaxCapacity = dto.MaxCapacity,
            DeliveryMode = dto.DeliveryMode,
            MeetingUrl = dto.MeetingUrl
        };
        await _repository.AddAsync(session);
        return CreatedAtAction(nameof(Get), new { id = session.Id }, MapToDto(session));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UpdateClassSessionDto dto)
    {
        var session = await _repository.GetByIdAsync(id);
        if (session == null) return NotFound();

        session.YogaClassId = dto.YogaClassId;
        session.RoomId = dto.RoomId;
        session.InstructorUserId = dto.InstructorUserId;
        session.StartTime = dto.StartTime;
        session.DurationMinutes = dto.DurationMinutes;
        session.MaxCapacity = dto.MaxCapacity;
        session.DeliveryMode = dto.DeliveryMode;
        session.MeetingUrl = dto.MeetingUrl;

        await _repository.UpdateAsync(session);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var session = await _repository.GetByIdAsync(id);
        if (session == null) return NotFound();
        await _repository.DeleteAsync(session);
        return NoContent();
    }

    private static ClassSessionDto MapToDto(ClassSession cs)
    {
        return new ClassSessionDto
        {
            Id = cs.Id,
            YogaClassId = cs.YogaClassId,
            RoomId = cs.RoomId,
            InstructorUserId = cs.InstructorUserId,
            StartTime = cs.StartTime,
            DurationMinutes = cs.DurationMinutes,
            MaxCapacity = cs.MaxCapacity,
            DeliveryMode = cs.DeliveryMode,
            MeetingUrl = cs.MeetingUrl,
            CreatedAt = cs.CreatedAt,
            UpdatedAt = cs.UpdatedAt
        };
    }
}
