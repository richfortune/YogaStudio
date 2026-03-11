using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaStudio.Api.Controllers;
using YogaStudio.Core.Entities;
using YogaStudio.Core.DTOs;
using YogaStudio.Core.Interfaces;

namespace YogaStudio.Api.Tests;

public class BookingsControllerTests
{
    private readonly Mock<IRepository<Booking>> _mockRepo;
    private readonly BookingsController _controller;

    public BookingsControllerTests()
    {
        _mockRepo = new Mock<IRepository<Booking>>();
        _controller = new BookingsController(_mockRepo.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfBookings()
    {
        // Arrange
        var mockData = new List<Booking> { new Booking { Id = Guid.NewGuid(), StudentUserId = Guid.NewGuid(), SessionId = Guid.NewGuid() } };
        _mockRepo.Setup(repo => repo.ListAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockData);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IReadOnlyList<BookingDto>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenBookingDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Booking?)null);

        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenBookingExists()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var mockBooking = new Booking { Id = bookingId };
        _mockRepo.Setup(repo => repo.GetByIdAsync(bookingId, It.IsAny<CancellationToken>())).ReturnsAsync(mockBooking);

        // Act
        var result = await _controller.Get(bookingId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<BookingDto>(okResult.Value);
        Assert.Equal(bookingId, returnValue.Id);
    }

    [Fact]
    public async Task Post_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newBookingDto = new CreateBookingDto { StudentUserId = Guid.NewGuid(), SessionId = Guid.NewGuid() };

        // Act
        var result = await _controller.Post(newBookingDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<BookingDto>(createdResult.Value);
        Assert.NotEqual(Guid.Empty, returnValue.Id);
        _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_ReturnsNotFound_WhenBookingDoesNotExist()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var updateDto = new UpdateBookingDto();
        _mockRepo.Setup(repo => repo.GetByIdAsync(bookingId, It.IsAny<CancellationToken>())).ReturnsAsync((Booking?)null);

        // Act
        var result = await _controller.Put(bookingId, updateDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenValidUpdate()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var booking = new Booking { Id = bookingId };
        var updateDto = new UpdateBookingDto();
        _mockRepo.Setup(repo => repo.GetByIdAsync(bookingId, It.IsAny<CancellationToken>())).ReturnsAsync(booking);

        // Act
        var result = await _controller.Put(bookingId, updateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.UpdateAsync(booking, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenBookingDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Booking?)null);

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenBookingDeleted()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var mockBooking = new Booking { Id = bookingId };
        _mockRepo.Setup(repo => repo.GetByIdAsync(bookingId, It.IsAny<CancellationToken>())).ReturnsAsync(mockBooking);

        // Act
        var result = await _controller.Delete(bookingId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.DeleteAsync(mockBooking, It.IsAny<CancellationToken>()), Times.Once);
    }
}
