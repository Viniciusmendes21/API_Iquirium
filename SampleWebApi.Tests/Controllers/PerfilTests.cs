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
    public class PerfilTests
    {
        private readonly Mock<IPerfilRepository> _mockPerfilRepo;
        private readonly PerfilController _controller;

        public PerfilTests()
        {
            _mockPerfilRepo = new Mock<IPerfilRepository>();
            _controller = new PerfilController(_mockPerfilRepo.Object);
        }

        [Fact]
        public async Task shouldListAllPerfis()
        {
            var perfis = new List<Perfil>
            {
                new Perfil { IdPerfil = 1, Nome = "Admin" },
                new Perfil { IdPerfil = 2, Nome = "User" }
            };

            _mockPerfilRepo.Setup(repo => repo.GetPerfil()).ReturnsAsync(perfis);

            var result = await _controller.GetPerfil();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<Perfil>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task shouldReturnOkWhenPerfilExistsById()
        {
            var perfil = new Perfil { IdPerfil = 1, Nome = "Admin" };

            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync(perfil);

            var result = await _controller.GetPerfilById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Perfil>(okResult.Value);
            Assert.Equal(1, returnValue.IdPerfil);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenPerfilDoesNotExistById()
        {
            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync((Perfil)null);

            var result = await _controller.GetPerfilById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnCreatedAtActionWhenPostPerfilIsValid()
        {
            var perfil = new Perfil { Nome = "User" };

            _mockPerfilRepo.Setup(repo => repo.AddPerfil(It.IsAny<Perfil>())).Returns(Task.CompletedTask);

            var result = await _controller.PostPerfil(perfil);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetPerfilById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenPostPerfilIsInvalid()
        {
            var perfil = new Perfil { Nome = "" };

            _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório");

            var result = await _controller.PostPerfil(perfil);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenPerfilIsUpdated()
        {
            var perfil = new Perfil { IdPerfil = 1, Nome = "Updated Admin" };
            var existingPerfil = new Perfil { IdPerfil = 1, Nome = "Admin" };

            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync(existingPerfil);
            _mockPerfilRepo.Setup(repo => repo.UpdatePerfil(It.IsAny<Perfil>())).Returns(Task.CompletedTask);

            var result = await _controller.PutPerfil(1, perfil);

            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenPerfilDoesNotExistForUpdate()
        {
            var perfil = new Perfil { IdPerfil = 1, Nome = "Updated Admin" };

            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync((Perfil)null);

            var result = await _controller.PutPerfil(1, perfil);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenPerfilIsDeleted()
        {
            var perfil = new Perfil { IdPerfil = 1, Nome = "Admin" };

            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync(perfil);
            _mockPerfilRepo.Setup(repo => repo.DeletePerfil(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeletePerfil(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenPerfilDoesNotExistForDeletion()
        {
            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync((Perfil)null);

            var result = await _controller.DeletePerfil(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
