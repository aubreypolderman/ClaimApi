using Xunit;
using ClaimApi.Repository;
using Moq;
using ClaimApi.Controllers;
using ClaimApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApiTest;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new UsersController(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResultSave()
    {
        // Arrange
        var users = new List<User> { new User { Id = 1, Name = "John" } };
        _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
        Assert.Single(model);
    }

    // Write similar tests for other controller actions (GetUser, CreateUser, UpdateUser, DeleteUser, GetUserByEmail)
    [Fact]
    public async Task GetUsers_ReturnsOkResult()
    {
        // Arrange
        var users = new[] { new User { Id = 1, Name = "John" } };
        _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<User[]>(okResult.Value);
        Assert.Single(model);
    }

    [Fact]
    public async Task GetUser_ReturnsOkResult()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John" };
        _userRepositoryMock.Setup(repo => repo.GetUser(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user, model);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFoundResult()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetUser(1)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John" };
        _userRepositoryMock.Setup(repo => repo.CreateUser(user)).ReturnsAsync(user);

        // Act
        var result = await _controller.CreateUser(user);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetUser", createdAtActionResult.ActionName);
        Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        var model = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal(user, model);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContentResult()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John" };
        _userRepositoryMock.Setup(repo => repo.UpdateUser(user)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateUser(1, user);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNotFoundResult()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John" };
        _userRepositoryMock.Setup(repo => repo.UpdateUser(user)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateUser(1, user);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContentResult()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.DeleteUser(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNotFoundResult()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.DeleteUser(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsOkResult()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmail("john@example.com")).ReturnsAsync(user);

        // Act
        var result = await _controller.GetUserByEmail("john@example.com");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user, model);
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsNotFoundResult()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetUserByEmail("john@example.com")).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetUserByEmail("john@example.com");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found.", notFoundResult.Value);
    }

    // Additional code to configure the UserRepository mock
    private IUserRepository ConfigureUserRepositoryMock()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        // Configure mock setup and return behavior as needed
        return userRepositoryMock.Object;
    }
}
