using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackUsuarioController : ControllerBase
    {
        private readonly IFeedbackUsuarioRepository _feedbackRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITipoFeedbackUsuarioRepository _tipoFeedbackUsuarioRepository;

        public FeedbackUsuarioController(
            IFeedbackUsuarioRepository feedbackRepository,
            IUsuarioRepository usuarioRepository,
            ITipoFeedbackUsuarioRepository tipoFeedbackUsuarioRepository)
        {
            _feedbackRepository = feedbackRepository;
            _usuarioRepository = usuarioRepository;
            _tipoFeedbackUsuarioRepository = tipoFeedbackUsuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbackUsuario()
        {
            var result = await _feedbackRepository.GetFeedbackUsuario();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackUsuarioById(int id)
        {
            var result = await _feedbackRepository.GetFeedbackUsuarioByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedbackUsuario([FromBody] FeedbackUsuario feedbackUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioEnvio = await _usuarioRepository.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioEnvio);
            var usuarioDestino = await _usuarioRepository.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioDestino ?? 0);
            var usuarioIntermedio = await _usuarioRepository.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioIntermedio ?? 0);
            var tipoFeedbackUsuario = await _tipoFeedbackUsuarioRepository.GetTipoFeedbackUsuarioByIdAsync(feedbackUsuario.IdTipoFeedbackUsuario ?? 0);

            if (usuarioEnvio == null)
                return BadRequest($"O IdUsuarioEnvio {feedbackUsuario.IdUsuarioEnvio} não existe.");

            if (feedbackUsuario.IdUsuarioDestino.HasValue && usuarioDestino == null)
                return BadRequest($"O IdUsuarioDestino {feedbackUsuario.IdUsuarioDestino} não existe.");

            if (feedbackUsuario.IdUsuarioIntermedio.HasValue && usuarioIntermedio == null)
                return BadRequest($"O IdUsuarioIntermedio {feedbackUsuario.IdUsuarioIntermedio} não existe.");

            if (tipoFeedbackUsuario == null)
                return BadRequest($"O IdTipoFeedbackUsuario {feedbackUsuario.IdTipoFeedbackUsuario} não existe.");

            try
            {
                await _feedbackRepository.AddFeedbackUsuario(feedbackUsuario);
                return CreatedAtAction(nameof(GetFeedbackUsuarioById), new { id = feedbackUsuario.IdFeedbackUsuario }, feedbackUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackUsuario(int id, [FromBody] FeedbackUsuario feedbackUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFeedback = await _feedbackRepository.GetFeedbackUsuarioByIdAsync(id);
            if (existingFeedback == null)
            {
                return NotFound();
            }

            var usuarioEnvio = await _usuarioRepository.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioEnvio);
            var usuarioDestino = await _usuarioRepository.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioDestino ?? 0);
            var usuarioIntermedio = await _usuarioRepository.GetUsuarioByIdAsync(feedbackUsuario.IdUsuarioIntermedio ?? 0);
            var tipoFeedbackUsuario = await _tipoFeedbackUsuarioRepository.GetTipoFeedbackUsuarioByIdAsync(feedbackUsuario.IdTipoFeedbackUsuario ?? 0);

            if (usuarioEnvio == null)
                return BadRequest($"O IdUsuarioEnvio {feedbackUsuario.IdUsuarioEnvio} não existe.");

            if (feedbackUsuario.IdUsuarioDestino.HasValue && usuarioDestino == null)
                return BadRequest($"O IdUsuarioDestino {feedbackUsuario.IdUsuarioDestino} não existe.");

            if (feedbackUsuario.IdUsuarioIntermedio.HasValue && usuarioIntermedio == null)
                return BadRequest($"O IdUsuarioIntermedio {feedbackUsuario.IdUsuarioIntermedio} não existe.");

            if (tipoFeedbackUsuario == null)
                return BadRequest($"O IdTipoFeedbackUsuario {feedbackUsuario.IdTipoFeedbackUsuario} não existe.");

            // Atualiza os campos do feedback
            existingFeedback.IdUsuarioEnvio = feedbackUsuario.IdUsuarioEnvio;
            existingFeedback.IdUsuarioDestino = feedbackUsuario.IdUsuarioDestino;
            existingFeedback.IdUsuarioIntermedio = feedbackUsuario.IdUsuarioIntermedio;
            existingFeedback.IdTipoFeedbackUsuario = feedbackUsuario.IdTipoFeedbackUsuario;
            existingFeedback.StatusFeedback = feedbackUsuario.StatusFeedback;
            existingFeedback.Mensagem = feedbackUsuario.Mensagem;
            existingFeedback.DataAprovacao = feedbackUsuario.DataAprovacao;

            await _feedbackRepository.UpdateFeedbackUsuario(existingFeedback);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackUsuario(int id)
        {
            var result = await _feedbackRepository.GetFeedbackUsuarioByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _feedbackRepository.DeleteFeedbackUsuario(id);

            return NoContent();
        }
    }
}
