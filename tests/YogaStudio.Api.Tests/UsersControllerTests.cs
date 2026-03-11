using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaStudio.Api.Controllers;
using YogaStudio.Core.Entities;
using YogaStudio.Core.DTOs;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Tests;

public class UsersControllerTests
{
    private readonly Mock<IRepository<User>> _mockRepo;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mockRepo = new Mock<IRepository<User>>();
        _controller = new UsersController(_mockRepo.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfUsers()
    {
        // Arrange
        var mockUsers = new List<User> { new User { Id = Guid.NewGuid(), FirstName = "Test", Email = "test@test.com" } };
        _mockRepo.Setup(repo => repo.ListAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockUsers);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IReadOnlyList<UserDto>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockUser = new User { Id = userId, FirstName = "Test" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(mockUser);

        // Act
        var result = await _controller.Get(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(userId, returnValue.Id);
    }

    [Fact]
    public async Task Post_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newUserDto = new CreateUserDto { FirstName = "New", LastName = "User", Email = "test@test.com" };

        // Act
        var result = await _controller.Post(newUserDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<UserDto>(createdResult.Value);
        Assert.NotEqual(Guid.Empty, returnValue.Id);
        _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updateDto = new UpdateUserDto();
        _mockRepo.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

        // Act
        var result = await _controller.Put(userId, updateDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenValidUpdate()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var existingUser = new User { Id = userId };
        var updateDto = new UpdateUserDto { FirstName = "Updated" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(existingUser);

        // Act
        var result = await _controller.Put(userId, updateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.UpdateAsync(existingUser, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenUserDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
        _mockRepo.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);

        // Act
        var result = await _controller.Delete(userId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.DeleteAsync(user, It.IsAny<CancellationToken>()), Times.Once);
    }
}
