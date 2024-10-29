using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;
using SampleWebApi.Repository;
using SampleWebApi.Repository.Interface;


namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            var result = await _usuarioRepository.GetUsuario();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var result = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid || !usuario.IdPerfil.HasValue)
            {
                // Retorna os erros de validação, incluindo os definidos nas anotações de dados
                return BadRequest(ModelState);
            }

            // Verifica se o IdPerfil existe diretamente chamando GetPerfilByIdAsync
            var perfil = await _perfilRepository.GetPerfilByIdAsync(usuario.IdPerfil.Value);
            if (perfil == null)
            {
                return BadRequest($"O IdPerfil {usuario.IdPerfil.Value} não existe.");
            }

            try
            {
                await _usuarioRepository.AddUsuario(usuario);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.IdUsuario }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid || !usuario.IdPerfil.HasValue)
            {
                // Retorna os erros de validação, incluindo os definidos nas anotações de dados
                return BadRequest(ModelState);
            }

            var checkUsuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (checkUsuario == null)
            {
                return NotFound();
            }

            // Verifica se o IdPerfil existe diretamente chamando GetPerfilByIdAsync
            var perfil = await _perfilRepository.GetPerfilByIdAsync(usuario.IdPerfil.Value);
            if (perfil == null)
            {
                return BadRequest($"O IdPerfil {usuario.IdPerfil.Value} não existe.");
            }

            checkUsuario.IdPerfil = usuario.IdPerfil.Value;
            checkUsuario.Nome = usuario.Nome;

            await _usuarioRepository.UpdateUsuario(checkUsuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _usuarioRepository.DeleteUsuario(id);

            return NoContent();
        }
        
    }

}
