using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;
using Microsoft.AspNetCore.Mvc;


namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoFeedbackProdutoController : ControllerBase
    {
        private readonly ITipoFeedbackProdutoRepository _tipoFeedbackRepository;

        public TipoFeedbackProdutoController(ITipoFeedbackProdutoRepository tipoFeedbackRepository)
        {
            _tipoFeedbackRepository = tipoFeedbackRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTipoFeedbackProduto()
        {
            var result = await _tipoFeedbackRepository.GetTipoFeedbackProduto();
            
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoFeedbackProdutoById(int id)
        {
            var result = await _tipoFeedbackRepository.GetTipoFeedbackProdutoByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostTipoFeedbackProduto([FromBody] TipoFeedbackProduto tipoFeedbackProduto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tipoFeedbackRepository.AddTipoFeedbackProduto(tipoFeedbackProduto);
                return CreatedAtAction(nameof(GetTipoFeedbackProdutoById), new { id = tipoFeedbackProduto.IdTipoFeedbackProduto }, tipoFeedbackProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoFeedbackProduto(int id, [FromBody] TipoFeedbackProduto tipoFeedbackProduto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkTipoFeedback = await _tipoFeedbackRepository.GetTipoFeedbackProdutoByIdAsync(id);
            if (checkTipoFeedback == null)
            {
                return NotFound();
            }

            checkTipoFeedback.Nome = tipoFeedbackProduto.Nome;

            await _tipoFeedbackRepository.UpdateTipoFeedbackProduto(checkTipoFeedback);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoFeedbackProduto(int id)
        {
            var result = await _tipoFeedbackRepository.GetTipoFeedbackProdutoByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _tipoFeedbackRepository.DeleteTipoFeedbackProduto(id);

            return NoContent();
        }
        
    }

}
