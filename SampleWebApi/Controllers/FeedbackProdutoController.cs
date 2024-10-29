using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Repository;


namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackProdutoController : ControllerBase
    {
        private readonly IFeedbackProdutoRepository _feedbackRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITipoFeedbackProdutoRepository _tipoFeedbackProdutoRepository;

        public FeedbackProdutoController(IFeedbackProdutoRepository feedbackRepository, IUsuarioRepository usuarioRepository, ITipoFeedbackProdutoRepository tipoFeedbackProdutoRepository)
        {
            _feedbackRepository = feedbackRepository;
            _usuarioRepository = usuarioRepository;
            _tipoFeedbackProdutoRepository = tipoFeedbackProdutoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbackProduto()
        {
            var result = await _feedbackRepository.GetFeedbackProduto();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackProdutoById(int id)
        {
            var result = await _feedbackRepository.GetFeedbackProdutoByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedbackProduto([FromBody] FeedbackProduto feedbackProduto)
        {
            if (!ModelState.IsValid || !feedbackProduto.IdUsuario.HasValue || !feedbackProduto.IdTipoFeedbackProduto.HasValue)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(feedbackProduto.IdUsuario.Value);
            var tipoFeedbackProduto = await _tipoFeedbackProdutoRepository.GetTipoFeedbackProdutoByIdAsync(feedbackProduto.IdTipoFeedbackProduto.Value);

            if (usuario == null && tipoFeedbackProduto != null)
            {
                return BadRequest($"O IdUsuario {feedbackProduto.IdUsuario.Value} não existe.");
            }

            if (usuario != null && tipoFeedbackProduto == null)
            {
                return BadRequest($"O IdTipoFeedbackProduto {feedbackProduto.IdTipoFeedbackProduto.Value} não existe.");
            }

            if (usuario == null && tipoFeedbackProduto == null)
            {
                return BadRequest($"O IdUsuario {feedbackProduto.IdUsuario.Value} e IdTipoFeedbackProduto {feedbackProduto.IdTipoFeedbackProduto.Value} não existe.");
            }

            try
            {
                await _feedbackRepository.AddFeedbackProduto(feedbackProduto);
                return CreatedAtAction(nameof(GetFeedbackProdutoById), new { id = feedbackProduto.IdFeedbackProduto }, feedbackProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackProduto(int id, [FromBody] FeedbackProduto feedbackProduto)
        {
            if (!ModelState.IsValid || !feedbackProduto.IdUsuario.HasValue || !feedbackProduto.IdTipoFeedbackProduto.HasValue)
            {
                return BadRequest(ModelState);
            }

            var checkFeedback = await _feedbackRepository.GetFeedbackProdutoByIdAsync(id);
            if (checkFeedback == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(feedbackProduto.IdUsuario.Value);
            var tipoFeedbackProduto = await _tipoFeedbackProdutoRepository.GetTipoFeedbackProdutoByIdAsync(feedbackProduto.IdTipoFeedbackProduto.Value);
            
            if (usuario == null && tipoFeedbackProduto != null)
            {
                return BadRequest($"O IdUsuario {feedbackProduto.IdUsuario.Value} não existe.");
            }

            if (usuario != null && tipoFeedbackProduto == null)
            {
                return BadRequest($"O IdTipoFeedbackProduto {feedbackProduto.IdTipoFeedbackProduto.Value} não existe.");
            }

            if(usuario == null && tipoFeedbackProduto == null)
            {
                return BadRequest($"O IdUsuario {feedbackProduto.IdUsuario.Value} e IdTipoFeedbackProduto {feedbackProduto.IdTipoFeedbackProduto.Value} não existe.");
            }

            // Atualiza os campos do feedback
            checkFeedback.IdTipoFeedbackProduto = feedbackProduto.IdTipoFeedbackProduto.Value;
            checkFeedback.IdUsuario = feedbackProduto.IdUsuario.Value;
            checkFeedback.Comentario = feedbackProduto.Comentario;
            checkFeedback.DataEnvio = feedbackProduto.DataEnvio;

            await _feedbackRepository.UpdateFeedbackProduto(checkFeedback);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackProduto(int id)
        {
            var result= await _feedbackRepository.GetFeedbackProdutoByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _feedbackRepository.DeleteFeedbackProduto(id);

            return NoContent();
        }

    }

}
