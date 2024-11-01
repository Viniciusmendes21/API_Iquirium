using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;


namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilController(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPerfil()
        {
            var result = await _perfilRepository.GetPerfil();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerfilById(int id)
        {
            var result = await _perfilRepository.GetPerfilByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostPerfil([FromBody] Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _perfilRepository.AddPerfil(perfil);
                return CreatedAtAction(nameof(GetPerfilById), new { id = perfil.IdPerfil }, perfil);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfil(int id, [FromBody] Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkPerfil = await _perfilRepository.GetPerfilByIdAsync(id);
            if (checkPerfil == null)
            {
                return NotFound();
            }

            checkPerfil.Nome = perfil.Nome;

            await _perfilRepository.UpdatePerfil(checkPerfil);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            var result = await _perfilRepository.GetPerfilByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _perfilRepository.DeletePerfil(id);

            return NoContent();
        }
        
    }

}
