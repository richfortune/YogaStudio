using Microsoft.EntityFrameworkCore;
using Moq;
using YogaStudio.Core.Entities;
using YogaStudio.Infrastructure;
using YogaStudio.Infrastructure.Repositories;

namespace YogaStudio.Infrastructure.Tests;

public class RepositoryTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Repository<User> _repository;

    public RepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _repository = new Repository<User>(_dbContext);
    }

    [Fact]
    public async Task AddAsync_AddsEntityToDatabase()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john@example.com" };

        // Act
        var addedUser = await _repository.AddAsync(user);

        // Assert
        Assert.NotNull(addedUser);
        var dbUser = await _dbContext.Users.FindAsync(user.Id);
        Assert.NotNull(dbUser);
        Assert.Equal("John", dbUser.FirstName);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEntity_WhenEntityExists()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), FirstName = "Jane", Email = "jane@example.com" };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenEntityDoesNotExist()
    {
        // Act
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ListAllAsync_ReturnsAllEntities()
    {
        // Arrange
        _dbContext.Users.AddRange(
            new User { Id = Guid.NewGuid(), FirstName = "User1", Email = "user1@example.com" },
            new User { Id = Guid.NewGuid(), FirstName = "User2", Email = "user2@example.com" }
        );
        await _dbContext.SaveChangesAsync();

        // Act
        var results = await _repository.ListAllAsync();

        // Assert
        Assert.Equal(2, results.Count);
    }

    [Fact]
    public async Task UpdateAsync_ModifiesEntityInDatabase()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), FirstName = "OldName", Email = "old@example.com" };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Act
        user.FirstName = "NewName";
        await _repository.UpdateAsync(user);

        // Assert
        var updatedUser = await _dbContext.Users.FindAsync(user.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal("NewName", updatedUser.FirstName);
    }

    [Fact]
    public async Task DeleteAsync_RemovesEntityFromDatabase()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), FirstName = "ToDelete", Email = "delete@example.com" };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(user);

        // Assert
        var deletedUser = await _dbContext.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);
    }
}
