using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaimApi.Controllers;
using ClaimApi.Model;
using ClaimApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ClaimApiTest;

public class ContractsControllerTests
{
    private readonly Mock<IContractRepository> _contractRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly ContractsController _controller;

    public ContractsControllerTests()
    {
        _contractRepositoryMock = new Mock<IContractRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new ContractsController(_contractRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetContracts_ReturnsOkResult()
    {
        // Arrange
        var contracts = new[]
        {
            new Contract { Id = 1, Product = "Product 1", UserId = 1 },
            new Contract { Id = 2, Product = "Product 2", UserId = 2 }
        };
        var users = new[]
        {
            new User { Id = 1, Name = "John" },
            new User { Id = 2, Name = "Jane" }
        };
        _contractRepositoryMock.Setup(repo => repo.GetAllContracts()).ReturnsAsync(contracts);
        _userRepositoryMock.Setup(repo => repo.GetUser(It.IsAny<int>()))
            .Returns<int>(id => Task.FromResult(users.FirstOrDefault(u => u.Id == id)));

        // Act
        var result = await _controller.GetContracts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ContractDto>>(okResult.Value);
        Assert.Equal(contracts.Length, model.Count());
    }

    [Fact]
    public async Task GetContract_ReturnsOkResult()
    {
        // Arrange
        var contract = new Contract { Id = 1, Product = "Product 1", UserId = 1 };
        var user = new User { Id = 1, Name = "John" };
        _contractRepositoryMock.Setup(repo => repo.GetContract(1)).ReturnsAsync(contract);
        _userRepositoryMock.Setup(repo => repo.GetUser(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetContract(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<ContractDto>(okResult.Value);
        Assert.Equal(contract.Id, model.Id);
        Assert.Equal(contract.Product, model.Product);
        Assert.Equal(user, model.User);
    }

    [Fact]
    public async Task GetContract_ReturnsNotFoundResult()
    {
        // Arrange
        _contractRepositoryMock.Setup(repo => repo.GetContract(1)).ReturnsAsync((Contract)null);

        // Act
        var result = await _controller.GetContract(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("Contract not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task CreateContract_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var contract = new Contract { Id = 1, Product = "Product 1", UserId = 1, User = new User { Id = 1, Name = "John" } };
        _userRepositoryMock.Setup(repo => repo.GetUser(1)).ReturnsAsync(contract.User);
        _contractRepositoryMock.Setup(repo => repo.CreateContract(contract)).ReturnsAsync(contract);

        // Act
        var result = await _controller.CreateContract(contract);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsType<Contract>(createdAtActionResult.Value);
        Assert.Equal(contract.Id, model.Id);
        Assert.Equal(contract.Product, model.Product);
    }

    [Fact]
    public async Task CreateContract_ReturnsBadRequestResult()
    {
        // Arrange
        var contract = new Contract { Id = 1, Product = "Product 1", UserId = 1, User = new User { Id = 1, Name = "John" } };
        _userRepositoryMock.Setup(repo => repo.GetUser(1)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.CreateContract(contract);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("User does not exist", badRequestResult.Value);
    }

    [Fact]
    public async Task UpdateContract_ReturnsNoContentResult()
    {
        // Arrange
        var contract = new Contract { Id = 1, Product = "Product 1", UserId = 1 };
        _contractRepositoryMock.Setup(repo => repo.UpdateContract(contract)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateContract(1, contract);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateContract_ReturnsNotFoundResult()
    {
        // Arrange
        var contract = new Contract { Id = 1, Product = "Product 1", UserId = 1 };
        _contractRepositoryMock.Setup(repo => repo.UpdateContract(contract)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateContract(1, contract);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteContract_ReturnsNoContentResult()
    {
        // Arrange
        _contractRepositoryMock.Setup(repo => repo.DeleteContract(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteContract(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteContract_ReturnsNotFoundResult()
    {
        // Arrange
        _contractRepositoryMock.Setup(repo => repo.DeleteContract(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteContract(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetContractsByUserId_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;
        var contracts = new[]
        {
            new Contract { Id = 1, Product = "Product 1", UserId = 1 },
            new Contract { Id = 2, Product = "Product 2", UserId = 1 }
        };
        var user = new User { Id = 1, Name = "John" };
        _contractRepositoryMock.Setup(repo => repo.GetContractsByUserId(userId)).ReturnsAsync(contracts);
        _userRepositoryMock.Setup(repo => repo.GetUser(It.IsAny<int>()))
            .Returns<int>(id => Task.FromResult(user));

        // Act
        var result = await _controller.GetContractsByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ContractDto>>(okResult.Value);
        Assert.Equal(contracts.Length, model.Count());
    }
}
