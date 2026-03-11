using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaStudio.Api.Controllers;
using YogaStudio.Core.Entities;
using YogaStudio.Core.DTOs;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Tests;

public class YogaClassesControllerTests
{
    private readonly Mock<IRepository<YogaClass>> _mockRepo;
    private readonly YogaClassesController _controller;

    public YogaClassesControllerTests()
    {
        _mockRepo = new Mock<IRepository<YogaClass>>();
        _controller = new YogaClassesController(_mockRepo.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfClasses()
    {
        // Arrange
        var mockData = new List<YogaClass> { new YogaClass { Id = Guid.NewGuid(), Name = "Hatha Yoga" } };
        _mockRepo.Setup(repo => repo.ListAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockData);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IReadOnlyList<YogaClassDto>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenYogaClassDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((YogaClass?)null);

        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenYogaClassExists()
    {
        // Arrange
        var classId = Guid.NewGuid();
        var mockClass = new YogaClass { Id = classId, Name = "Vinyasa" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(classId, It.IsAny<CancellationToken>())).ReturnsAsync(mockClass);

        // Act
        var result = await _controller.Get(classId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<YogaClassDto>(okResult.Value);
        Assert.Equal(classId, returnValue.Id);
    }

    [Fact]
    public async Task Post_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newClassDto = new CreateYogaClassDto { Name = "New Class" };

        // Act
        var result = await _controller.Post(newClassDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<YogaClassDto>(createdResult.Value);
        Assert.NotEqual(Guid.Empty, returnValue.Id);
        _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<YogaClass>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_ReturnsNotFound_WhenYogaClassDoesNotExist()
    {
        // Arrange
        var classId = Guid.NewGuid();
        var updateDto = new UpdateYogaClassDto();
        _mockRepo.Setup(repo => repo.GetByIdAsync(classId, It.IsAny<CancellationToken>())).ReturnsAsync((YogaClass?)null);

        // Act
        var result = await _controller.Put(classId, updateDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenValidUpdate()
    {
        // Arrange
        var classId = Guid.NewGuid();
        var yogaClass = new YogaClass { Id = classId };
        var updateDto = new UpdateYogaClassDto { Name = "Updated Class" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(classId, It.IsAny<CancellationToken>())).ReturnsAsync(yogaClass);

        // Act
        var result = await _controller.Put(classId, updateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.UpdateAsync(yogaClass, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenYogaClassDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((YogaClass?)null);

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenYogaClassDeleted()
    {
        // Arrange
        var classId = Guid.NewGuid();
        var mockClass = new YogaClass { Id = classId };
        _mockRepo.Setup(repo => repo.GetByIdAsync(classId, It.IsAny<CancellationToken>())).ReturnsAsync(mockClass);

        // Act
        var result = await _controller.Delete(classId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.DeleteAsync(mockClass, It.IsAny<CancellationToken>()), Times.Once);
    }
}
