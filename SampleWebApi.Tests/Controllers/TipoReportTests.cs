using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleWebApi.Controllers;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebApi.Tests.Controllers
{
    public class TipoReportTests
    {
        private readonly Mock<ITipoReportRepository> _mockTipoReportRepo;
        private readonly TipoReportController _controller;

        public TipoReportTests()
        {
            _mockTipoReportRepo = new Mock<ITipoReportRepository>();
            _controller = new TipoReportController(_mockTipoReportRepo.Object);
        }

        [Fact]
        public async Task shouldListAllTipoReport()
        {
            var tipoReports = new List<TipoReport>
            {
                new TipoReport { IdTipoReport = 1, Nome = "Relatório Anual" },
                new TipoReport { IdTipoReport = 2, Nome = "Relatório Mensal" }
            };

            _mockTipoReportRepo.Setup(repo => repo.GetTipoReport()).ReturnsAsync(tipoReports);

            var result = await _controller.GetTipoReport();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<TipoReport>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task shouldReturnOkWhenTipoReportExistsById()
        {
            var tipoReport = new TipoReport { IdTipoReport = 1, Nome = "Relatório Anual" };

            _mockTipoReportRepo.Setup(repo => repo.GetTipoReportByIdAsync(1)).ReturnsAsync(tipoReport);

            var result = await _controller.GetTipoReportById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TipoReport>(okResult.Value);
            Assert.Equal(1, returnValue.IdTipoReport);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoReportDoesNotExistById()
        {
            _mockTipoReportRepo.Setup(repo => repo.GetTipoReportByIdAsync(1)).ReturnsAsync((TipoReport)null);

            var result = await _controller.GetTipoReportById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnCreatedAtActionWhenPostTipoReportIsValid()
        {
            var tipoReport = new TipoReport { Nome = "Relatório Anual" };

            _mockTipoReportRepo.Setup(repo => repo.AddTipoReport(It.IsAny<TipoReport>())).Returns(Task.CompletedTask);

            var result = await _controller.PostTipoReport(tipoReport);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTipoReportById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenPostTipoReportIsInvalid()
        {
            var tipoReport = new TipoReport { Nome = "" };
            _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório");

            var result = await _controller.PostTipoReport(tipoReport);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenTipoReportIsUpdated()
        {
            var tipoReport = new TipoReport { IdTipoReport = 1, Nome = "Relatório Atualizado" };
            var existingTipoReport = new TipoReport { IdTipoReport = 1, Nome = "Relatório Antigo" };

            _mockTipoReportRepo.Setup(repo => repo.GetTipoReportByIdAsync(1)).ReturnsAsync(existingTipoReport);
            _mockTipoReportRepo.Setup(repo => repo.UpdateTipoReport(It.IsAny<TipoReport>())).Returns(Task.CompletedTask);

            var result = await _controller.PutTipoReport(1, tipoReport);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoReportDoesNotExistForUpdate()
        {
            var tipoReport = new TipoReport { IdTipoReport = 1, Nome = "Relatório Atualizado" };

            _mockTipoReportRepo.Setup(repo => repo.GetTipoReportByIdAsync(1)).ReturnsAsync((TipoReport)null);

            var result = await _controller.PutTipoReport(1, tipoReport);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenTipoReportIsDeleted()
        {
            var tipoReport = new TipoReport { IdTipoReport = 1, Nome = "Relatório a Ser Excluído" };

            _mockTipoReportRepo.Setup(repo => repo.GetTipoReportByIdAsync(1)).ReturnsAsync(tipoReport);
            _mockTipoReportRepo.Setup(repo => repo.DeleteTipoReport(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteTipoReport(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoReportDoesNotExistForDeletion()
        {
            _mockTipoReportRepo.Setup(repo => repo.GetTipoReportByIdAsync(1)).ReturnsAsync((TipoReport)null);

            var result = await _controller.DeleteTipoReport(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
