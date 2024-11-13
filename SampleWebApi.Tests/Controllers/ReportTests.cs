using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleWebApi.Controllers;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebApi.Tests.Controllers
{
    public class ReportTests
    {
        private readonly Mock<IReportRepository> _mockReportRepository;
        private readonly Mock<IFeedbackUsuarioRepository> _mockFeedbackUsuarioRepository;
        private readonly Mock<ITipoReportRepository> _mockTipoReportRepository;
        private readonly ReportController _controller;

        public ReportTests()
        {
            _mockReportRepository = new Mock<IReportRepository>();
            _mockFeedbackUsuarioRepository = new Mock<IFeedbackUsuarioRepository>();
            _mockTipoReportRepository = new Mock<ITipoReportRepository>();
            _controller = new ReportController(
                _mockReportRepository.Object,
                _mockFeedbackUsuarioRepository.Object,
                _mockTipoReportRepository.Object
            );
        }

        [Fact]
        public async Task ShouldListAllReports()
        {
            var reports = new List<Report>
            {
                new Report { IdReport = 1, IdFeedbackUsuario = 1, IdTipoReport = 1, Deferido = true },
                new Report { IdReport = 2, IdFeedbackUsuario = 2, IdTipoReport = 1, Deferido = false }
            };

            _mockReportRepository.Setup(repo => repo.GetReport()).ReturnsAsync(reports);

            var result = await _controller.GetReports();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<Report>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task ShouldReturnOkWhenReportExistsById()
        {
            var report = new Report
            {
                IdReport = 1,
                IdFeedbackUsuario = 1,
                IdTipoReport = 1,
                Deferido = true
            };

            _mockReportRepository.Setup(repo => repo.GetReportByIdAsync(1)).ReturnsAsync(report);

            var result = await _controller.GetReportById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Report>(okResult.Value);
            Assert.Equal(1, returnValue.IdReport);
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenReportDoesNotExistById()
        {
            _mockReportRepository.Setup(repo => repo.GetReportByIdAsync(1)).ReturnsAsync((Report)null);

            var result = await _controller.GetReportById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ShouldReturnCreatedAtActionWhenPostReportIsValid()
        {
            var report = new Report
            {
                IdFeedbackUsuario = 1,
                IdTipoReport = 1,
                Deferido = true
            };

            var feedbackUsuario = new FeedbackUsuario { IdFeedbackUsuario = 1 };
            var tipoReport = new TipoReport { IdTipoReport = 1 };

            _mockFeedbackUsuarioRepository.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(1)).ReturnsAsync(feedbackUsuario);
            _mockTipoReportRepository.Setup(repo => repo.GetTipoReportByIdAsync(1)).ReturnsAsync(tipoReport);
            _mockReportRepository.Setup(repo => repo.AddReport(It.IsAny<Report>())).Returns(Task.CompletedTask);

            var result = await _controller.PostReport(report);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetReportById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task ShouldReturnBadRequestWhenPostReportIsInvalid()
        {
            var report = new Report
            {
                IdFeedbackUsuario = null,
                IdTipoReport = 1,
                Deferido = true
            };

            _controller.ModelState.AddModelError("IdFeedbackUsuario", "O campo IdFeedbackUsuario é obrigatório");

            var result = await _controller.PostReport(report);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ShouldReturnNoContentWhenReportIsUpdated()
        {
            var report = new Report
            {
                IdReport = 1,
                IdFeedbackUsuario = 1,
                IdTipoReport = 1,
                Deferido = false
            };

            var existingReport = new Report
            {
                IdReport = 1,
                IdFeedbackUsuario = 1,
                IdTipoReport = 1,
                Deferido = true
            };

            var feedbackUsuario = new FeedbackUsuario { IdFeedbackUsuario = 1 };
            var tipoReport = new TipoReport { IdTipoReport = 1 };

            _mockReportRepository.Setup(repo => repo.GetReportByIdAsync(report.IdReport)).ReturnsAsync(existingReport);
            _mockFeedbackUsuarioRepository.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(report.IdFeedbackUsuario.Value)).ReturnsAsync(feedbackUsuario);
            _mockTipoReportRepository.Setup(repo => repo.GetTipoReportByIdAsync(report.IdTipoReport.Value)).ReturnsAsync(tipoReport);
            _mockReportRepository.Setup(repo => repo.UpdateReport(It.IsAny<Report>())).Returns(Task.CompletedTask);

            var result = await _controller.PutReport(report.IdReport, report);

            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task ShouldReturnNotFoundWhenReportDoesNotExistForUpdate()
        {
            var report = new Report
            {
                IdReport = 1,
                IdFeedbackUsuario = 1,
                IdTipoReport = 1,
                Deferido = true
            };

            _mockReportRepository.Setup(repo => repo.GetReportByIdAsync(1)).ReturnsAsync((Report)null);

            var result = await _controller.PutReport(1, report);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ShouldReturnNoContentWhenReportIsDeleted()
        {
            var report = new Report
            {
                IdReport = 1,
                IdFeedbackUsuario = 1,
                IdTipoReport = 1,
                Deferido = true
            };

            _mockReportRepository.Setup(repo => repo.GetReportByIdAsync(1)).ReturnsAsync(report);
            _mockReportRepository.Setup(repo => repo.DeleteReport(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteReport(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenReportDoesNotExistForDeletion()
        {
            _mockReportRepository.Setup(repo => repo.GetReportByIdAsync(1)).ReturnsAsync((Report)null);

            var result = await _controller.DeleteReport(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
