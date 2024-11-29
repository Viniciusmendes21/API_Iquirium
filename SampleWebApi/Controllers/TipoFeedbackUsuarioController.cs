using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoFeedbackUsuarioController : ControllerBase
    {
        private readonly ITipoFeedbackUsuarioRepository _tipoFeedbackUsuarioRepository;

        public TipoFeedbackUsuarioController(ITipoFeedbackUsuarioRepository tipoFeedbackUsuarioRepository)
        {
            _tipoFeedbackUsuarioRepository = tipoFeedbackUsuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTipoFeedbackUsuario()
        {
            var result = await _tipoFeedbackUsuarioRepository.GetTipoFeedbackUsuario();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoFeedbackUsuarioById(int id)
        {
            var result = await _tipoFeedbackUsuarioRepository.GetTipoFeedbackUsuarioByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostTipoFeedbackUsuario([FromBody] TipoFeedbackUsuario tipoFeedbackUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tipoFeedbackUsuarioRepository.AddTipoFeedbackUsuario(tipoFeedbackUsuario);
                return CreatedAtAction(nameof(GetTipoFeedbackUsuarioById), new { id = tipoFeedbackUsuario.IdTipoFeedbackUsuario }, tipoFeedbackUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoFeedbackUsuario(int id, [FromBody] TipoFeedbackUsuario tipoFeedbackUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkTipoFeedbackUsuario = await _tipoFeedbackUsuarioRepository.GetTipoFeedbackUsuarioByIdAsync(id);
            if (checkTipoFeedbackUsuario == null)
            {
                return NotFound();
            }

            checkTipoFeedbackUsuario.Nome = tipoFeedbackUsuario.Nome;

            await _tipoFeedbackUsuarioRepository.UpdateTipoFeedbackUsuario(checkTipoFeedbackUsuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoFeedbackUsuario(int id)
        {
            var result = await _tipoFeedbackUsuarioRepository.GetTipoFeedbackUsuarioByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _tipoFeedbackUsuarioRepository.DeleteTipoFeedbackUsuario(id);

            return NoContent();
        }

    }
}
