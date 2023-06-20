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

public class RepairCompaniesControllerTests
{
    private readonly Mock<IRepairCompanyRepository> _repairCompanyRepositoryMock;
    private readonly RepairCompaniesController _controller;

    public RepairCompaniesControllerTests()
    {
        _repairCompanyRepositoryMock = new Mock<IRepairCompanyRepository>();
        _controller = new RepairCompaniesController(_repairCompanyRepositoryMock.Object);
    }

    [Fact]
    public async Task GetRepairCompanies_ReturnsOkResult()
    {
        // Arrange
        var repairCompanies = new[]
        {
                new RepairCompany { Id = 1, Name = "Company 1" },
                new RepairCompany { Id = 2, Name = "Company 2" }
            };
        _repairCompanyRepositoryMock.Setup(repo => repo.GetAllRepairCompanies()).ReturnsAsync(repairCompanies);

        // Act
        var result = await _controller.GetRepairCompanies();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<RepairCompany>>(okResult.Value);
        Assert.Equal(repairCompanies.Length, model.Count());
    }

    [Fact]
    public async Task GetRepairCompany_ReturnsOkResult()
    {
        // Arrange
        var repairCompany = new RepairCompany { Id = 1, Name = "Company 1" };
        _repairCompanyRepositoryMock.Setup(repo => repo.GetRepairCompany(1)).ReturnsAsync(repairCompany);

        // Act
        var result = await _controller.GetRepairCompany(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<RepairCompany>(okResult.Value);
        Assert.Equal(repairCompany.Id, model.Id);
        Assert.Equal(repairCompany.Name, model.Name);
    }

    [Fact]
    public async Task GetRepairCompany_ReturnsNotFoundResult()
    {
        // Arrange
        _repairCompanyRepositoryMock.Setup(repo => repo.GetRepairCompany(1)).ReturnsAsync((RepairCompany)null);

        // Act
        var result = await _controller.GetRepairCompany(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("RepairCompany not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task CreateRepairCompany_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var repairCompany = new RepairCompany { Id = 1, Name = "Company 1" };
        _repairCompanyRepositoryMock.Setup(repo => repo.CreateRepairCompany(repairCompany)).ReturnsAsync(repairCompany);

        // Act
        var result = await _controller.CreateRepairCompany(repairCompany);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsType<RepairCompany>(createdAtActionResult.Value);
        Assert.Equal(repairCompany.Id, model.Id);
        Assert.Equal(repairCompany.Name, model.Name);
    }

    [Fact]
    public async Task UpdateRepairCompany_ReturnsNoContentResult()
    {
        // Arrange
        var repairCompany = new RepairCompany { Id = 1, Name = "Company 1" };
        _repairCompanyRepositoryMock.Setup(repo => repo.UpdateRepairCompany(repairCompany)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateRepairCompany(1, repairCompany);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateRepairCompany_ReturnsNotFoundResult()
    {
        // Arrange
        var repairCompany = new RepairCompany { Id = 1, Name = "Company 1" };
        _repairCompanyRepositoryMock.Setup(repo => repo.UpdateRepairCompany(repairCompany)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateRepairCompany(1, repairCompany);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteRepairCompany_ReturnsNoContentResult()
    {
        // Arrange
        _repairCompanyRepositoryMock.Setup(repo => repo.DeleteRepairCompany(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteRepairCompanyt(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteRepairCompany_ReturnsNotFoundResult()
    {
        // Arrange
        _repairCompanyRepositoryMock.Setup(repo => repo.DeleteRepairCompany(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteRepairCompanyt(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
