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
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepo;
        private readonly Mock<IPerfilRepository> _mockPerfilRepo;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();
            _mockPerfilRepo = new Mock<IPerfilRepository>();
            _controller = new UsuarioController(_mockUsuarioRepo.Object, _mockPerfilRepo.Object);
        }

        [Fact]
        public async Task shouldListAllUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { IdUsuario = 1, Nome = "User1", IdPerfil = 1 },
                new Usuario { IdUsuario = 2, Nome = "User2", IdPerfil = 2 }
            };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuario()).ReturnsAsync(usuarios);

            var result = await _controller.GetUsuario();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<Usuario>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task shouldReturnOkWhenUsuarioExistsById()
        {
            var usuario = new Usuario { IdUsuario = 1, Nome = "User1", IdPerfil = 1 };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync(usuario);

            var result = await _controller.GetUsuarioById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Usuario>(okResult.Value);
            Assert.Equal(1, returnValue.IdUsuario);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenUsuarioDoesNotExistById()
        {
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync((Usuario)null);

            var result = await _controller.GetUsuarioById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnCreatedAtActionWhenPostUsuarioIsValid()
        {
            var usuario = new Usuario { Nome = "User1", IdPerfil = 1 };

            var perfil = new Perfil { IdPerfil = 1, Nome = "Admin" };
            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync(perfil);
            _mockUsuarioRepo.Setup(repo => repo.AddUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var result = await _controller.PostUsuario(usuario);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetUsuarioById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenPostUsuarioIsInvalid()
        {
            var usuario = new Usuario { Nome = "", IdPerfil = 1 };  // Nome vazio (inválido)

            _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório");

            var result = await _controller.PostUsuario(usuario);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenPostUsuarioHasInvalidPerfil()
        {
            var usuario = new Usuario { Nome = "User1", IdPerfil = 999 };  // IdPerfil inexistente

            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(999)).ReturnsAsync((Perfil)null);

            var result = await _controller.PostUsuario(usuario);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task shouldReturnNoContentWhenUsuarioIsUpdated()
        {
            var usuario = new Usuario { IdUsuario = 1, Nome = "UpdatedUser", IdPerfil = 1 };
            var existingUsuario = new Usuario { IdUsuario = 1, Nome = "User1", IdPerfil = 1 };

            var perfil = new Perfil { IdPerfil = 1, Nome = "Admin" };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync(existingUsuario);
            _mockUsuarioRepo.Setup(repo => repo.UpdateUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(1)).ReturnsAsync(perfil);

            var result = await _controller.PutUsuario(1, usuario);

            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenUsuarioDoesNotExistForUpdate()
        {
            var usuario = new Usuario { IdUsuario = 1, Nome = "UpdatedUser", IdPerfil = 1 };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync((Usuario)null);

            var result = await _controller.PutUsuario(1, usuario);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task shouldReturnBadRequestWhenUsuarioUpdateHasInvalidPerfil()
        {
            var usuario = new Usuario
            {
                IdUsuario = 1,
                Nome = "Usuario Teste",
                IdPerfil = 999
            };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(usuario.IdUsuario)).ReturnsAsync(new Usuario { IdUsuario = 1 });
            _mockPerfilRepo.Setup(repo => repo.GetPerfilByIdAsync(usuario.IdPerfil.Value)).ReturnsAsync((Perfil)null);

            var result = await _controller.PutUsuario(usuario.IdUsuario, usuario);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("O IdPerfil 999 não existe.", badRequestResult.Value);
        }


        [Fact]
        public async Task shouldReturnNoContentWhenUsuarioIsDeleted()
        {
            var usuario = new Usuario { IdUsuario = 1, Nome = "User1", IdPerfil = 1 };

            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync(usuario);
            _mockUsuarioRepo.Setup(repo => repo.DeleteUsuario(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteUsuario(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task shouldReturnNotFoundWhenUsuarioDoesNotExistForDeletion()
        {
            _mockUsuarioRepo.Setup(repo => repo.GetUsuarioByIdAsync(1)).ReturnsAsync((Usuario)null);

            var result = await _controller.DeleteUsuario(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
