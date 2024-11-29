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
    public class TipoFeedbackProdutoTests
    {
        private readonly Mock<ITipoFeedbackProdutoRepository> _mockTipoFeedbackProdutoRepo;
        private readonly TipoFeedbackProdutoController _controller;

        public TipoFeedbackProdutoTests()
        {
            _mockTipoFeedbackProdutoRepo = new Mock<ITipoFeedbackProdutoRepository>();
            _controller = new TipoFeedbackProdutoController(
                _mockTipoFeedbackProdutoRepo.Object
            );
        }

        [Fact]
        public async Task shouldListAllTipoFeedbackProduto()
        {
            var tipoFeedbacks = new List<TipoFeedbackProduto>
            {
                new TipoFeedbackProduto { IdTipoFeedbackProduto = 1, Nome = "Feedback Positivo" },
                new TipoFeedbackProduto { IdTipoFeedbackProduto = 2, Nome = "Feedback Negativo" }
            };

            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProduto()).ReturnsAsync(tipoFeedbacks);

            var result = await _controller.GetTipoFeedbackProduto();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<TipoFeedbackProduto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task shouldReturnOkWhenTipoFeedbackProdutoExistsById()
        {
            var tipoFeedbackProduto = new TipoFeedbackProduto
            {
                IdTipoFeedbackProduto = 1,
                Nome = "Feedback Positivo"
            };

            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(1)).ReturnsAsync(tipoFeedbackProduto);

            var result = await _controller.GetTipoFeedbackProdutoById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TipoFeedbackProduto>(okResult.Value);
            Assert.Equal(1, returnValue.IdTipoFeedbackProduto);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoFeedbackProdutoDoesNotExistById()
        {
            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(1)).ReturnsAsync((TipoFeedbackProduto)null);

            var result = await _controller.GetTipoFeedbackProdutoById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnCreatedAtActionWhenPostTipoFeedbackProdutoIsValid()
        {
            var tipoFeedbackProduto = new TipoFeedbackProduto
            {
                Nome = "Feedback Positivo"
            };

            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.AddTipoFeedbackProduto(It.IsAny<TipoFeedbackProduto>())).Returns(Task.CompletedTask);

            var result = await _controller.PostTipoFeedbackProduto(tipoFeedbackProduto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTipoFeedbackProdutoById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenPostTipoFeedbackProdutoIsInvalid()
        {
            var tipoFeedbackProduto = new TipoFeedbackProduto
            {
                Nome = ""
            };

            _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório");

            var result = await _controller.PostTipoFeedbackProduto(tipoFeedbackProduto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenTipoFeedbackProdutoIsUpdated()
        {
            var tipoFeedbackProduto = new TipoFeedbackProduto
            {
                IdTipoFeedbackProduto = 1,
                Nome = "Feedback Atualizado"
            };

            var existingTipoFeedbackProduto = new TipoFeedbackProduto
            {
                IdTipoFeedbackProduto = 1,
                Nome = "Feedback Antigo"
            };

            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(tipoFeedbackProduto.IdTipoFeedbackProduto))
                                         .ReturnsAsync(existingTipoFeedbackProduto);
            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.UpdateTipoFeedbackProduto(It.IsAny<TipoFeedbackProduto>()))
                                         .Returns(Task.CompletedTask);
            var result = await _controller.PutTipoFeedbackProduto(tipoFeedbackProduto.IdTipoFeedbackProduto, tipoFeedbackProduto);

            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoFeedbackProdutoDoesNotExistForUpdate()
        {
            var tipoFeedbackProduto = new TipoFeedbackProduto
            {
                IdTipoFeedbackProduto = 1,
                Nome = "Feedback Atualizado"
            };

            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(1)).ReturnsAsync((TipoFeedbackProduto)null);

            var result = await _controller.PutTipoFeedbackProduto(1, tipoFeedbackProduto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenTipoFeedbackProdutoIsDeleted()
        {
            var tipoFeedbackProduto = new TipoFeedbackProduto
            {
                IdTipoFeedbackProduto = 1,
                Nome = "Feedback a Ser Excluído"
            };

            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(1)).ReturnsAsync(tipoFeedbackProduto);
            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.DeleteTipoFeedbackProduto(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteTipoFeedbackProduto(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenTipoFeedbackProdutoDoesNotExistForDeletion()
        {
            _mockTipoFeedbackProdutoRepo.Setup(repo => repo.GetTipoFeedbackProdutoByIdAsync(1)).ReturnsAsync((TipoFeedbackProduto)null);

            var result = await _controller.DeleteTipoFeedbackProduto(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
