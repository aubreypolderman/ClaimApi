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

public class ClaimFormsControllerTests
{
    private readonly Mock<IClaimFormRepository> _claimFormRepositoryMock;
    private readonly Mock<IContractRepository> _contractRepositoryMock;
    private readonly ClaimFormsController _controller;

    public ClaimFormsControllerTests()
    {
        _claimFormRepositoryMock = new Mock<IClaimFormRepository>();
        _contractRepositoryMock = new Mock<IContractRepository>();
        _controller = new ClaimFormsController(_claimFormRepositoryMock.Object, _contractRepositoryMock.Object);
    }

    [Fact]
    public async Task GetClaims_ReturnsOkResult()
    {
        // Arrange
        var claimForms = new[]
        {
                new ClaimForm { Id = 1, ContractId = 1 },
                new ClaimForm { Id = 2, ContractId = 2 }
            };
        var contracts = new[]
        {
                new Contract { Id = 1 },
                new Contract { Id = 2 }
            };
        _claimFormRepositoryMock.Setup(repo => repo.GetAllClaimForms()).ReturnsAsync(claimForms);
        _contractRepositoryMock.Setup(repo => repo.GetContract(1)).ReturnsAsync(contracts[0]);
        _contractRepositoryMock.Setup(repo => repo.GetContract(2)).ReturnsAsync(contracts[1]);

        // Act
        var result = await _controller.GetClaims();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ClaimFormDto>>(okResult.Value);
        Assert.Equal(claimForms.Length, model.Count());
        Assert.Equal(contracts[0], model.ElementAt(0).Contract);
        Assert.Equal(contracts[1], model.ElementAt(1).Contract);
    }

    [Fact]
    public async Task GetClaimsByUserId_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;
        var contracts = new[]
        {
                new Contract { Id = 1, UserId = userId },
                new Contract { Id = 2, UserId = userId }
            };
        var claimForms = new[]
        {
                new ClaimForm { Id = 1, ContractId = 1 },
                new ClaimForm { Id = 2, ContractId = 2 }
            };
        _contractRepositoryMock.Setup(repo => repo.GetContractsByUserId(userId)).ReturnsAsync(contracts);
        _claimFormRepositoryMock.Setup(repo => repo.GetClaimFormsByContractIds(new List<int> { 1, 2 })).ReturnsAsync(claimForms);

        // Act
        var result = await _controller.GetClaimsByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ClaimFormDto>>(okResult.Value);
        Assert.Equal(claimForms.Length, model.Count());
        Assert.Equal(contracts[0], model.ElementAt(0).Contract);
        Assert.Equal(contracts[1], model.ElementAt(1).Contract);
    }
    /*
    [Fact]
    public async Task CreateClaimForm_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var claimForm = new ClaimForm { Id = 1, ContractId = 1 };
        var contract = new Contract { Id = 1 };
        _claimFormRepositoryMock.Setup(repo => repo.CreateClaimForm(claimForm)).ReturnsAsync(claimForm);
        _contractRepositoryMock.Setup(repo => repo.GetContract(claimForm.ContractId)).ReturnsAsync(contract);

        // Act
        var result = await _controller.CreateClaimForm(claimForm);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(ClaimFormsController.CreateClaimForm), createdAtActionResult.ActionName);
        Assert.Equal(claimForm.Id, createdAtActionResult.RouteValues["id"]);
        Assert.Equal(claimForm, createdAtActionResult.Value);
    }
    */
    [Fact]
    public async Task UpdateClaimForm_ReturnsNoContentResult()
    {
        // Arrange
        var claimForm = new ClaimForm { Id = 1, ContractId = 1 };
        _claimFormRepositoryMock.Setup(repo => repo.UpdateClaimForm(claimForm)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateClaimForm(1, claimForm);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateClaimForm_ReturnsNotFoundResult()
    {
        // Arrange
        var claimForm = new ClaimForm { Id = 1, ContractId = 1 };
        _claimFormRepositoryMock.Setup(repo => repo.UpdateClaimForm(claimForm)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateClaimForm(1, claimForm);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteClaimForm_ReturnsNoContentResult()
    {
        // Arrange
        _claimFormRepositoryMock.Setup(repo => repo.DeleteClaimForm(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteClaimForm(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteClaimForm_ReturnsNotFoundResult()
    {
        // Arrange
        _claimFormRepositoryMock.Setup(repo => repo.DeleteClaimForm(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteClaimForm(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    /*
    [Fact]
    public async Task GetClaimForm_ReturnsOkResult()
    {
        // Arrange
        var claimForm = new ClaimForm { Id = 1, ContractId = 1 };
        var contract = new Contract { Id = 1 };
        _claimFormRepositoryMock.Setup(repo => repo.CreateClaimForm(claimForm)).ReturnsAsync(claimForm);
        _contractRepositoryMock.Setup(repo => repo.GetContract(claimForm.ContractId)).ReturnsAsync(contract);

        // Act
        var result = await _controller.CreateClaimForm(claimForm);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("CreateClaimForm", createdAtActionResult.ActionName);
        Assert.Equal(claimForm.Id, createdAtActionResult.RouteValues["id"]);
        Assert.Equal(claimForm, createdAtActionResult.Value);
    }
    */
    [Fact]
    public async Task GetClaimForm_ReturnsNotFoundResult_WhenClaimFormIsNull()
    {
        // Arrange
        _claimFormRepositoryMock.Setup(repo => repo.GetClaimForm(1)).ReturnsAsync((ClaimForm)null);

        // Act
        var result = await _controller.GetClaimForm(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("ClaimForm not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetClaimForm_ReturnsNotFoundResult_WhenContractIsNull()
    {
        // Arrange
        var claimForm = new ClaimForm { Id = 1, ContractId = 1 };
        _claimFormRepositoryMock.Setup(repo => repo.GetClaimForm(1)).ReturnsAsync(claimForm);
        _contractRepositoryMock.Setup(repo => repo.GetContract(claimForm.ContractId)).ReturnsAsync((Contract)null);

        // Act
        var result = await _controller.GetClaimForm(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("Contract not found.", notFoundResult.Value);
    }
}
