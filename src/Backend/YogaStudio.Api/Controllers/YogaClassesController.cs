using Microsoft.AspNetCore.Mvc;
using YogaStudio.Core.DTOs;
using YogaStudio.Core.Entities;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class YogaClassesController : ControllerBase
{
    private readonly IRepository<YogaClass> _repository;

    public YogaClassesController(IRepository<YogaClass> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<YogaClassDto>>> Get()
    {
        var classes = await _repository.ListAllAsync();
        var dtos = classes.Select(c => MapToDto(c)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<YogaClassDto>> Get(Guid id)
    {
        var yogaClass = await _repository.GetByIdAsync(id);
        if (yogaClass == null) return NotFound();
        return Ok(MapToDto(yogaClass));
    }

    [HttpPost]
    public async Task<ActionResult<YogaClassDto>> Post([FromBody] CreateYogaClassDto dto)
    {
        var yogaClass = new YogaClass
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            DifficultyLevel = dto.DifficultyLevel
        };
        await _repository.AddAsync(yogaClass);
        return CreatedAtAction(nameof(Get), new { id = yogaClass.Id }, MapToDto(yogaClass));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UpdateYogaClassDto dto)
    {
        var yogaClass = await _repository.GetByIdAsync(id);
        if (yogaClass == null) return NotFound();

        yogaClass.Name = dto.Name;
        yogaClass.Description = dto.Description;
        yogaClass.DifficultyLevel = dto.DifficultyLevel;

        await _repository.UpdateAsync(yogaClass);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var yogaClass = await _repository.GetByIdAsync(id);
        if (yogaClass == null) return NotFound();
        await _repository.DeleteAsync(yogaClass);
        return NoContent();
    }

    private static YogaClassDto MapToDto(YogaClass yc)
    {
        return new YogaClassDto
        {
            Id = yc.Id,
            Name = yc.Name,
            Description = yc.Description,
            DifficultyLevel = yc.DifficultyLevel,
            CreatedAt = yc.CreatedAt,
            UpdatedAt = yc.UpdatedAt
        };
    }
}
