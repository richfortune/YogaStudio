using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaStudio.Api.Controllers;
using YogaStudio.Core.Entities;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Tests;

public class ClassSessionsControllerTests
{
    private readonly Mock<IRepository<ClassSession>> _mockRepo;
    private readonly ClassSessionsController _controller;

    public ClassSessionsControllerTests()
    {
        _mockRepo = new Mock<IRepository<ClassSession>>();
        _controller = new ClassSessionsController(_mockRepo.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfSessions()
    {
        // Arrange
        var mockSessions = new List<ClassSession> { new ClassSession { Id = Guid.NewGuid() } };
        _mockRepo.Setup(repo => repo.ListAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockSessions);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IReadOnlyList<ClassSession>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenSessionDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((ClassSession?)null);

        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenSessionExists()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var mockSession = new ClassSession { Id = sessionId };
        _mockRepo.Setup(repo => repo.GetByIdAsync(sessionId, It.IsAny<CancellationToken>())).ReturnsAsync(mockSession);

        // Act
        var result = await _controller.Get(sessionId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ClassSession>(okResult.Value);
        Assert.Equal(sessionId, returnValue.Id);
    }

    [Fact]
    public async Task Post_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newSession = new ClassSession();

        // Act
        var result = await _controller.Post(newSession);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<ClassSession>(createdResult.Value);
        Assert.NotEqual(Guid.Empty, returnValue.Id);
        _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<ClassSession>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var session = new ClassSession { Id = Guid.NewGuid() };

        // Act
        var result = await _controller.Put(Guid.NewGuid(), session);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenValidUpdate()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new ClassSession { Id = sessionId };

        // Act
        var result = await _controller.Put(sessionId, session);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.UpdateAsync(session, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenSessionDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((ClassSession?)null);

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenSessionDeleted()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new ClassSession { Id = sessionId };
        _mockRepo.Setup(repo => repo.GetByIdAsync(sessionId, It.IsAny<CancellationToken>())).ReturnsAsync(session);

        // Act
        var result = await _controller.Delete(sessionId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.DeleteAsync(session, It.IsAny<CancellationToken>()), Times.Once);
    }
}
