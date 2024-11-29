using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleWebApi.Controllers;
using SampleWebApi.Models;
using SampleWebApi.Repository;
using SampleWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Tests.Controllers
{
    public class FeedbackUsuarioTests
    {
        private readonly Mock<IFeedbackUsuarioRepository> _mockFeedbackUsuarioRepo;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepo;
        private readonly Mock<ITipoFeedbackUsuarioRepository> _mockTipoFeedbackUsuarioRepo;
        private readonly FeedbackUsuarioController _controller;

        public FeedbackUsuarioTests()
        {
            _mockFeedbackUsuarioRepo = new Mock<IFeedbackUsuarioRepository>();
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();
            _mockTipoFeedbackUsuarioRepo = new Mock<ITipoFeedbackUsuarioRepository>();
            _controller = new FeedbackUsuarioController(
                _mockFeedbackUsuarioRepo.Object,
                _mockUsuarioRepo.Object,
                _mockTipoFeedbackUsuarioRepo.Object
            );
        }

        [Fact]
        public async Task shouldListAllFeedbackUsuario()
        {
            var feedbacks = new List<FeedbackUsuario>
            {
                new FeedbackUsuario { IdFeedbackUsuario = 1, IdUsuarioEnvio = 1, IdUsuarioDestino = 2, IdUsuarioIntermedio = 3, IdTipoFeedbackUsuario = 1, StatusFeedback = true, Mensagem = "Bom feedback", DataAprovacao = DateTime.UtcNow },
                new FeedbackUsuario { IdFeedbackUsuario = 2, IdUsuarioEnvio = 2, IdUsuarioDestino = 1, IdUsuarioIntermedio = 3, IdTipoFeedbackUsuario = 2, StatusFeedback = false, Mensagem = "Necessita melhorias", DataAprovacao = DateTime.UtcNow }
            };

            _mockFeedbackUsuarioRepo.Setup(repo => repo.GetFeedbackUsuario()).ReturnsAsync(feedbacks);

            var result = await _controller.GetFeedbackUsuario();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<FeedbackUsuario>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task shouldReturnOkWhenFeedbackUsuarioExistsById()
        {
            var feedback = new FeedbackUsuario
            {
                IdFeedbackUsuario = 1,
                IdUsuarioEnvio = 1,
                IdUsuarioDestino = 2,
                IdUsuarioIntermedio = 3,
                IdTipoFeedbackUsuario = 1,
                StatusFeedback = true,
                Mensagem = "Bom feedback",
                DataAprovacao = DateTime.UtcNow
            };

            _mockFeedbackUsuarioRepo.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(1)).ReturnsAsync(feedback);

            var result = await _controller.GetFeedbackUsuarioById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FeedbackUsuario>(okResult.Value);
            Assert.Equal(1, returnValue.IdFeedbackUsuario);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenFeedbackUsuarioDoesNotExistById()
        {
            _mockFeedbackUsuarioRepo.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(1)).ReturnsAsync((FeedbackUsuario)null);

            var result = await _controller.GetFeedbackUsuarioById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnCreatedAtActionWhenPostFeedbackUsuarioIsValid()
        {
            var feedbackUsuario = new FeedbackUsuario
            {
                IdUsuarioEnvio = 1,
                IdUsuarioDestino = 2,
                IdUsuarioIntermedio = 3,
                IdTipoFeedbackUsuario = 1,
                StatusFeedback = true,
                Mensagem = "Feedback excelente",
                DataAprovacao = DateTime.UtcNow
            };

            var usuarioEnvio = new Usuario { IdUsuario = 1 };
            var usuarioDestino = new Usuario { IdUsuario = 2 };
            var tipoFeedback = new TipoFeedbackUsuario { IdTipoFeedbackUsuario = 1 };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync(usuarioEnvio);
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(2)).ReturnsAsync(usuarioDestino);
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(3)).ReturnsAsync(new Usuario { IdUsuario = 3 });  // Mock para o intermediário
            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(1)).ReturnsAsync(tipoFeedback);
            _mockFeedbackUsuarioRepo.Setup(repo => repo.AddFeedbackUsuario(It.IsAny<FeedbackUsuario>())).Returns(Task.CompletedTask);

            var result = await _controller.PostFeedbackUsuario(feedbackUsuario);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetFeedbackUsuarioById", createdAtActionResult.ActionName);
        }


        [Fact]
        public async Task shouldReturnBadRequestWhenPostFeedbackUsuarioIsInvalid()
        {
            var feedbackUsuario = new FeedbackUsuario
            {
                IdUsuarioEnvio = 0,
                IdUsuarioDestino = 2,
                IdUsuarioIntermedio = 3,
                IdTipoFeedbackUsuario = 1,
                StatusFeedback = true,
                Mensagem = "Feedback inválido",
                DataAprovacao = DateTime.UtcNow
            };

            _controller.ModelState.AddModelError("IdUsuarioEnvio", "O campo IdUsuarioEnvio é obrigatório");

            var result = await _controller.PostFeedbackUsuario(feedbackUsuario);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenFeedbackUsuarioIsUpdated()
        {
            var feedbackUsuario = new FeedbackUsuario
            {
                IdFeedbackUsuario = 1,
                IdUsuarioEnvio = 1,
                IdUsuarioDestino = 2,
                IdUsuarioIntermedio = 3,  // Verifique se este ID está sendo mockado corretamente
                IdTipoFeedbackUsuario = 1,
                StatusFeedback = true,
                Mensagem = "Feedback atualizado",
                DataAprovacao = DateTime.UtcNow
            };

            var existingFeedback = new FeedbackUsuario
            {
                IdFeedbackUsuario = 1,
                IdUsuarioEnvio = 1,
                IdUsuarioDestino = 2,
                IdUsuarioIntermedio = 3,
                IdTipoFeedbackUsuario = 1,
                StatusFeedback = true,
                Mensagem = "Feedback anterior",
                DataAprovacao = DateTime.UtcNow
            };

            var usuarioEnvio = new Usuario { IdUsuario = 1 };
            var usuarioDestino = new Usuario { IdUsuario = 2 };
            var tipoFeedback = new TipoFeedbackUsuario { IdTipoFeedbackUsuario = 1 };

            // Mocking repositorios
            _mockFeedbackUsuarioRepo.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(feedbackUsuario.IdFeedbackUsuario))
                                     .ReturnsAsync(existingFeedback);
            _mockFeedbackUsuarioRepo.Setup(repo => repo.UpdateFeedbackUsuario(It.IsAny<FeedbackUsuario>()))
                                     .Returns(Task.CompletedTask);
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioEnvio)).ReturnsAsync(usuarioEnvio);
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioDestino.Value)).ReturnsAsync(usuarioDestino);
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioIntermedio.Value)).ReturnsAsync(new Usuario { IdUsuario = 3 }); // Mock do intermediário
            _mockTipoFeedbackUsuarioRepo.Setup(repo => repo.GetTipoFeedbackUsuarioByIdAsync(feedbackUsuario.IdTipoFeedbackUsuario.Value))
                                         .ReturnsAsync(tipoFeedback);

            var result = await _controller.PutFeedbackUsuario(feedbackUsuario.IdFeedbackUsuario, feedbackUsuario);

            // Verificar se o resultado é NoContent
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenFeedbackUsuarioDoesNotExistForUpdate()
        {
            var feedbackUsuario = new FeedbackUsuario
            {
                IdFeedbackUsuario = 1,
                IdUsuarioEnvio = 1,
                IdUsuarioDestino = 2,
                IdUsuarioIntermedio = 3,
                IdTipoFeedbackUsuario = 1,
                StatusFeedback = true,
                Mensagem = "Feedback atualizado",
                DataAprovacao = DateTime.UtcNow
            };

            _mockFeedbackUsuarioRepo.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(1)).ReturnsAsync((FeedbackUsuario)null);

            var result = await _controller.PutFeedbackUsuario(1, feedbackUsuario);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenFeedbackUsuarioIsDeleted()
        {
            var feedbackUsuario = new FeedbackUsuario
            {
                IdFeedbackUsuario = 1,
                IdUsuarioEnvio = 1,
                IdUsuarioDestino = 2,
                IdUsuarioIntermedio = 3,
                IdTipoFeedbackUsuario = 1,
                StatusFeedback = true,
                Mensagem = "Feedback removido",
                DataAprovacao = DateTime.UtcNow
            };

            _mockFeedbackUsuarioRepo.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(1)).ReturnsAsync(feedbackUsuario);
            _mockFeedbackUsuarioRepo.Setup(repo => repo.DeleteFeedbackUsuario(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteFeedbackUsuario(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenFeedbackUsuarioDoesNotExistForDeletion()
        {
            _mockFeedbackUsuarioRepo.Setup(repo => repo.GetFeedbackUsuarioByIdAsync(1)).ReturnsAsync((FeedbackUsuario)null);

            var result = await _controller.DeleteFeedbackUsuario(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
