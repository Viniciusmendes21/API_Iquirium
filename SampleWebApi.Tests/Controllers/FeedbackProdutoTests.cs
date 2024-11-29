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
    public class FeedbackProdutoTests
    {
        private readonly Mock<IFeedbackProdutoRepository> _mockFeedbackProdutoRepo;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepo;
        private readonly Mock<ITipoFeedbackProdutoRepository> _mockTipoFeedbackProdutoRepo;
        private readonly FeedbackProdutoController _controller;

        public FeedbackProdutoTests()
        {
            _mockFeedbackProdutoRepo = new Mock<IFeedbackProdutoRepository>();
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();
            _mockTipoFeedbackProdutoRepo = new Mock<ITipoFeedbackProdutoRepository>();
            _controller = new FeedbackProdutoController(
                _mockFeedbackProdutoRepo.Object,
                _mockUsuarioRepo.Object,
                _mockTipoFeedbackProdutoRepo.Object
            );
        }

        [Fact]
        public async Task shouldListAllFeedbackProduto()
        {
            var feedbacks = new List<FeedbackProduto>
            {
                new FeedbackProduto { IdFeedbackProduto = 1, Comentario = "Bom produto", IdUsuario = 1, IdTipoFeedbackProduto = 1, DataEnvio = DateTime.UtcNow },
                new FeedbackProduto { IdFeedbackProduto = 2, Comentario = "Excelente", IdUsuario = 2, IdTipoFeedbackProduto = 1, DataEnvio = DateTime.UtcNow }
            };

            _mockFeedbackProdutoRepo.Setup(repo => repo.GetFeedbackProduto()).ReturnsAsync(feedbacks);

            var result = await _controller.GetFeedbackProduto();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<FeedbackProduto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task shouldReturnOkWhenFeedbackProdutoExistsById()
        {
            var feedback = new FeedbackProduto
            {
                IdFeedbackProduto = 1,
                Comentario = "Bom produto",
                IdUsuario = 1,
                IdTipoFeedbackProduto = 1,
                DataEnvio = DateTime.UtcNow
            };

            _mockFeedbackProdutoRepo.Setup(repo => repo.GetFeedbackProdutoByIdAsync(1)).ReturnsAsync(feedback);

            var result = await _controller.GetFeedbackProdutoById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FeedbackProduto>(okResult.Value);
            Assert.Equal(1, returnValue.IdFeedbackProduto);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenFeedbackProdutoDoesNotExistById()
        {
            _mockFeedbackProdutoRepo.Setup(repo => repo.GetFeedbackProdutoByIdAsync(1)).ReturnsAsync((FeedbackProduto)null);

            var result = await _controller.GetFeedbackProdutoById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnCreatedAtActionWhenPostFeedbackProdutoIsValid()
        {
            var feedbackProduto = new FeedbackProduto
            {
                IdUsuario = 1,
                IdTipoFeedbackProduto = 1,
                Comentario = "Excelente",
                DataEnvio = DateTime.UtcNow
            };

            var usuario = new Usuario { IdUsuario = 1 };
            var tipoFeedbackProduto = new TipoFeedbackProduto { IdTipoFeedbackProduto = 1 };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync(usuario);
            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(1)).ReturnsAsync(tipoFeedbackProduto);
            _mockFeedbackProdutoRepo.Setup(repo => repo.AddFeedbackProduto(It.IsAny<FeedbackProduto>())).Returns(Task.CompletedTask);

            var result = await _controller.PostFeedbackProduto(feedbackProduto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetFeedbackProdutoById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenPostFeedbackProdutoIsInvalid()
        {
            var feedbackProduto = new FeedbackProduto
            {
                IdUsuario = 0,
                IdTipoFeedbackProduto = 1,
                Comentario = "Excelente",
                DataEnvio = DateTime.UtcNow
            };

            _controller.ModelState.AddModelError("IdUsuario", "O campo IDUsuario é obrigatório");

            var result = await _controller.PostFeedbackProduto(feedbackProduto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenFeedbackProdutoIsUpdated()
        {
            var feedbackProduto = new FeedbackProduto
            {
                IdFeedbackProduto = 1,
                IdUsuario = 1,
                IdTipoFeedbackProduto = 1,
                Comentario = "Produto melhorado",
                DataEnvio = DateTime.UtcNow
            };

            var existingFeedback = new FeedbackProduto
            {
                IdFeedbackProduto = 1,
                IdUsuario = 1,
                IdTipoFeedbackProduto = 1,
                Comentario = "Produto melhoradoo",
                DataEnvio = DateTime.UtcNow
            };

            var usuario = new Usuario { IdUsuario = 1 };
            var tipoFeedbackProduto = new TipoFeedbackProduto { IdTipoFeedbackProduto = 1 };

            _mockFeedbackProdutoRepo.Setup(repo => repo.GetFeedbackProdutoByIdAsync(feedbackProduto.IdFeedbackProduto))
                                     .ReturnsAsync(existingFeedback);
            _mockFeedbackProdutoRepo.Setup(repo => repo.UpdateFeedbackProduto(It.IsAny<FeedbackProduto>()))
                                     .Returns(Task.CompletedTask);
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(feedbackProduto.IdUsuario.Value))
                            .ReturnsAsync(usuario); // Garantir que o usuário exista
            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(feedbackProduto.IdTipoFeedbackProduto.Value))
                                         .ReturnsAsync(tipoFeedbackProduto);

            var result = await _controller.PutFeedbackProduto(feedbackProduto.IdFeedbackProduto, feedbackProduto);

            var noContentResult = Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task shouldReturnNotFoundWhenFeedbackProdutoDoesNotExistForUpdate()
        {
            var feedbackProduto = new FeedbackProduto
            {
                IdFeedbackProduto = 1,
                IdUsuario = 1,
                IdTipoFeedbackProduto = 1,
                Comentario = "Produto atualizado",
                DataEnvio = DateTime.UtcNow
            };

            _mockFeedbackProdutoRepo.Setup(repo => repo.GetFeedbackProdutoByIdAsync(1)).ReturnsAsync((FeedbackProduto)null);

            var result = await _controller.PutFeedbackProduto(1, feedbackProduto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenFeedbackProdutoIsDeleted()
        {
            var feedbackProduto = new FeedbackProduto
            {
                IdFeedbackProduto = 1,
                IdUsuario = 1,
                IdTipoFeedbackProduto = 1,
                Comentario = "Produto ruim",
                DataEnvio = DateTime.UtcNow
            };

            _mockFeedbackProdutoRepo.Setup(repo => repo.GetFeedbackProdutoByIdAsync(1)).ReturnsAsync(feedbackProduto);
            _mockFeedbackProdutoRepo.Setup(repo => repo.DeleteFeedbackProduto(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteFeedbackProduto(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenFeedbackProdutoDoesNotExistForDeletion()
        {
            _mockFeedbackProdutoRepo.Setup(repo => repo.GetFeedbackProdutoByIdAsync(1)).ReturnsAsync((FeedbackProduto)null);

            var result = await _controller.DeleteFeedbackProduto(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
