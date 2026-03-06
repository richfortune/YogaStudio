using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IReadOnlyList<ClassSession>>> Get()
    {
        var sessions = await _repository.ListAllAsync();
        return Ok(sessions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassSession>> Get(Guid id)
    {
        var session = await _repository.GetByIdAsync(id);
        if (session == null) return NotFound();
        return Ok(session);
    }

    [HttpPost]
    public async Task<ActionResult<ClassSession>> Post([FromBody] ClassSession session)
    {
        session.Id = Guid.NewGuid();
        await _repository.AddAsync(session);
        return CreatedAtAction(nameof(Get), new { id = session.Id }, session);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ClassSession session)
    {
        if (id != session.Id) return BadRequest();
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
}
