using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaStudio.Api.Controllers;
using YogaStudio.Core.Entities;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Tests;

public class RoomsControllerTests
{
    private readonly Mock<IRepository<Room>> _mockRepo;
    private readonly RoomsController _controller;

    public RoomsControllerTests()
    {
        _mockRepo = new Mock<IRepository<Room>>();
        _controller = new RoomsController(_mockRepo.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfRooms()
    {
        // Arrange
        var mockRooms = new List<Room> { new Room { Id = Guid.NewGuid(), Name = "Studio A" } };
        _mockRepo.Setup(repo => repo.ListAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockRooms);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IReadOnlyList<Room>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenRoomDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Room?)null);

        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenRoomExists()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var mockRoom = new Room { Id = roomId, Name = "Studio A" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(roomId, It.IsAny<CancellationToken>())).ReturnsAsync(mockRoom);

        // Act
        var result = await _controller.Get(roomId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Room>(okResult.Value);
        Assert.Equal(roomId, returnValue.Id);
    }

    [Fact]
    public async Task Post_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newRoom = new Room { Name = "Studio B" };

        // Act
        var result = await _controller.Post(newRoom);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<Room>(createdResult.Value);
        Assert.NotEqual(Guid.Empty, returnValue.Id);
        _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var room = new Room { Id = Guid.NewGuid() };

        // Act
        var result = await _controller.Put(Guid.NewGuid(), room);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenValidUpdate()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId };

        // Act
        var result = await _controller.Put(roomId, room);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.UpdateAsync(room, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenRoomDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Room?)null);

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenRoomDeleted()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId };
        _mockRepo.Setup(repo => repo.GetByIdAsync(roomId, It.IsAny<CancellationToken>())).ReturnsAsync(room);

        // Act
        var result = await _controller.Delete(roomId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.DeleteAsync(room, It.IsAny<CancellationToken>()), Times.Once);
    }
}
