using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoReportController : ControllerBase
    {
        private readonly ITipoReportRepository _tipoReportRepository;

        public TipoReportController(ITipoReportRepository tipoReportRepository)
        {
            _tipoReportRepository = tipoReportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTipoReport()
        {
            var result = await _tipoReportRepository.GetTipoReport();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoReportById(int id)
        {
            var result = await _tipoReportRepository.GetTipoReportByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostTipoReport([FromBody] TipoReport tipoReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tipoReportRepository.AddTipoReport(tipoReport);
                return CreatedAtAction(nameof(GetTipoReportById), new { id = tipoReport.IdTipoReport }, tipoReport);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoReport(int id, [FromBody] TipoReport tipoReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkTipoReport = await _tipoReportRepository.GetTipoReportByIdAsync(id);
            if (checkTipoReport == null)
            {
                return NotFound();
            }

            checkTipoReport.Nome = tipoReport. Nome;

            await _tipoReportRepository.UpdateTipoReport(checkTipoReport);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoReport(int id)
        {
            var result = await _tipoReportRepository.GetTipoReportByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _tipoReportRepository.DeleteTipoReport(id);

            return NoContent();
        }

    }
}
