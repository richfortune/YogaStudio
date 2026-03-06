using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IReadOnlyList<YogaClass>>> Get()
    {
        var classes = await _repository.ListAllAsync();
        return Ok(classes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<YogaClass>> Get(Guid id)
    {
        var yogaClass = await _repository.GetByIdAsync(id);
        if (yogaClass == null) return NotFound();
        return Ok(yogaClass);
    }

    [HttpPost]
    public async Task<ActionResult<YogaClass>> Post([FromBody] YogaClass yogaClass)
    {
        yogaClass.Id = Guid.NewGuid();
        await _repository.AddAsync(yogaClass);
        return CreatedAtAction(nameof(Get), new { id = yogaClass.Id }, yogaClass);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] YogaClass yogaClass)
    {
        if (id != yogaClass.Id) return BadRequest();
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
}
