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
    public class TipoFeedbackUsuarioTests
    {
        private readonly Mock<ITipoFeedbackUsuarioRepository> _mockTipoFeedbackUsuarioRepo;
        private readonly TipoFeedbackUsuarioController _controller;

        public TipoFeedbackUsuarioTests()
        {
            _mockTipoFeedbackUsuarioRepo = new Mock<ITipoFeedbackUsuarioRepository>();
            _controller = new TipoFeedbackUsuarioController(
                _mockTipoFeedbackUsuarioRepo.Object
            );
        }

        [Fact]
        public async Task shouldListAllTipoFeedbackUsuario()
        {
            var tipoFeedbacks = new List<TipoFeedbackUsuario>
            {
                new TipoFeedbackUsuario { IdTipoFeedbackUsuario = 1, Nome = "Feedback Positivo" },
                new TipoFeedbackUsuario { IdTipoFeedbackUsuario = 2, Nome = "Feedback Negativo" }
            };

            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuario()).ReturnsAsync(tipoFeedbacks);

            var result = await _controller.GetTipoFeedbackUsuario();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<TipoFeedbackUsuario>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task shouldReturnOkWhenTipoFeedbackUsuarioExistsById()
        {
            var tipoFeedbackUsuario = new TipoFeedbackUsuario
            {
                IdTipoFeedbackUsuario = 1,
                Nome = "Feedback Positivo"
            };

            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(1)).ReturnsAsync(tipoFeedbackUsuario);

            var result = await _controller.GetTipoFeedbackUsuarioById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TipoFeedbackUsuario>(okResult.Value);
            Assert.Equal(1, returnValue.IdTipoFeedbackUsuario);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoFeedbackUsuarioDoesNotExistById()
        {
            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(1)).ReturnsAsync((TipoFeedbackUsuario)null);

            var result = await _controller.GetTipoFeedbackUsuarioById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnCreatedAtActionWhenPostTipoFeedbackUsuarioIsValid()
        {
            var tipoFeedbackUsuario = new TipoFeedbackUsuario
            {
                Nome = "Feedback Positivo"
            };

            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.AddTipoFeedbackUsuario(It.IsAny<TipoFeedbackUsuario>())).Returns(Task.CompletedTask);

            var result = await _controller.PostTipoFeedbackUsuario(tipoFeedbackUsuario);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTipoFeedbackUsuarioById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenPostTipoFeedbackUsuarioIsInvalid()
        {
            var tipoFeedbackUsuario = new TipoFeedbackUsuario
            {
                Nome = ""
            };

            _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório");

            var result = await _controller.PostTipoFeedbackUsuario(tipoFeedbackUsuario);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenTipoFeedbackUsuarioIsUpdated()
        {
            var tipoFeedbackUsuario = new TipoFeedbackUsuario
            {
                IdTipoFeedbackUsuario = 1,
                Nome = "Feedback Atualizado"
            };

            var existingTipoFeedbackUsuario = new TipoFeedbackUsuario
            {
                IdTipoFeedbackUsuario = 1,
                Nome = "Feedback Antigo"
            };

            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(tipoFeedbackUsuario.IdTipoFeedbackUsuario))
                                         .ReturnsAsync(existingTipoFeedbackUsuario);
            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.UpdateTipoFeedbackUsuario(It.IsAny<TipoFeedbackUsuario>()))
                                         .Returns(Task.CompletedTask);

            var result = await _controller.PutTipoFeedbackUsuario(tipoFeedbackUsuario.IdTipoFeedbackUsuario, tipoFeedbackUsuario);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoFeedbackUsuarioDoesNotExistForUpdate()
        {
            var tipoFeedbackUsuario = new TipoFeedbackUsuario
            {
                IdTipoFeedbackUsuario = 1,
                Nome = "Feedback Atualizado"
            };

            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(1)).ReturnsAsync((TipoFeedbackUsuario)null);

            var result = await _controller.PutTipoFeedbackUsuario(1, tipoFeedbackUsuario);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenTipoFeedbackUsuarioIsDeleted()
        {
            var tipoFeedbackUsuario = new TipoFeedbackUsuario
            {
                IdTipoFeedbackUsuario = 1,
                Nome = "Feedback a Ser Excluído"
            };

            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(1)).ReturnsAsync(tipoFeedbackUsuario);
            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.DeleteTipoFeedbackUsuario(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteTipoFeedbackUsuario(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoFeedbackUsuarioDoesNotExistForDeletion()
        {
            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(1)).ReturnsAsync((TipoFeedbackUsuario)null);

            var result = await _controller.DeleteTipoFeedbackUsuario(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
