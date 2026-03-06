using Microsoft.AspNetCore.Mvc;
using YogaStudio.Core.DTOs;
using YogaStudio.Core.Entities;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _repository;

    public UsersController(IRepository<User> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> Get()
    {
        var users = await _repository.ListAllAsync();
        var dtos = users.Select(u => MapToDto(u)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(MapToDto(user));
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Post([FromBody] CreateUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            IsActive = true
        };
        await _repository.AddAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, MapToDto(user));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UpdateUserDto dto)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.Phone = dto.Phone;
        user.IsActive = dto.IsActive;

        await _repository.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();
        await _repository.DeleteAsync(user);
        return NoContent();
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}
