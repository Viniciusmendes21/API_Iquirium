using API_Iquirium.Models;
using API_Iquirium.Repositories;
using API_Iquirium.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_Iquirium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackProdutoController : ControllerBase
    {
        private readonly IFeedbackProdutoRepository _feedbackRepository;

        public FeedbackProdutoController(IFeedbackProdutoRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
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
        public async Task<IActionResult> PostFeebackProduto([FromBody] FeedbackProduto feedbackProduto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkFeedback = await _feedbackRepository.GetFeedbackProdutoByIdAsync(id);
            if (checkFeedback == null)
            {
                return NotFound();
            }

            checkFeedback.IdTipoFeedbackProduto = feedbackProduto.IdTipoFeedbackProduto;
            checkFeedback.IdUsuario = feedbackProduto.IdUsuario;
            checkFeedback.Comentario = feedbackProduto.Comentario;

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
