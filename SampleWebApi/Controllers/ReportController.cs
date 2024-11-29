using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IFeedbackUsuarioRepository _feedbackUsuarioRepository;
        private readonly ITipoReportRepository _tipoReportRepository;

        public ReportController(IReportRepository reportRepository, IFeedbackUsuarioRepository feedbackUsuarioRepository, ITipoReportRepository tipoReportRepository)
        {
            _reportRepository = reportRepository;
            _feedbackUsuarioRepository = feedbackUsuarioRepository;
            _tipoReportRepository = tipoReportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var result = await _reportRepository.GetReport();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var result = await _reportRepository.GetReportByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostReport([FromBody] Report report)
        {
            if (!ModelState.IsValid || !report.IdFeedbackUsuario.HasValue || !report.IdTipoReport.HasValue)
            {
                return BadRequest(ModelState);
            }

            var feedbackUsuario = await _feedbackUsuarioRepository.GetFeedbackUsuarioByIdAsync(report.IdFeedbackUsuario.Value);
            var tipoReport = await _tipoReportRepository.GetTipoReportByIdAsync(report.IdTipoReport.Value);

            if (feedbackUsuario == null && tipoReport != null)
            {
                return BadRequest($"O IdFeedbackUsuario {report.IdFeedbackUsuario.Value} não existe.");
            }

            if (feedbackUsuario != null && tipoReport == null)
            {
                return BadRequest($"O IdTipoReport {report.IdTipoReport.Value} não existe.");
            }

            if (feedbackUsuario == null && tipoReport == null)
            {
                return BadRequest($"O IdFeedbackUsuario {report.IdFeedbackUsuario.Value} e IdTipoReport {report.IdTipoReport.Value} não existem.");
            }

            try
            {
                await _reportRepository.AddReport(report);
                return CreatedAtAction(nameof(GetReportById), new { id = report.IdReport }, report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, [FromBody] Report report)
        {
            if (!ModelState.IsValid || !report.IdFeedbackUsuario.HasValue || !report.IdTipoReport.HasValue)
            {
                return BadRequest(ModelState);
            }

            var checkReport = await _reportRepository.GetReportByIdAsync(id);
            if (checkReport == null)
            {
                return NotFound();
            }

            var feedbackUsuario = await _feedbackUsuarioRepository.GetFeedbackUsuarioByIdAsync(report.IdFeedbackUsuario.Value);
            var tipoReport = await _tipoReportRepository.GetTipoReportByIdAsync(report.IdTipoReport.Value);

            if (feedbackUsuario == null && tipoReport != null)
            {
                return BadRequest($"O IdFeedbackUsuario {report.IdFeedbackUsuario.Value} não existe.");
            }

            if (feedbackUsuario != null && tipoReport == null)
            {
                return BadRequest($"O IdTipoReport {report.IdTipoReport.Value} não existe.");
            }

            if (feedbackUsuario == null && tipoReport == null)
            {
                return BadRequest($"O IdFeedbackUsuario {report.IdFeedbackUsuario.Value} e IdTipoReport {report.IdTipoReport.Value} não existem.");
            }

            // Atualiza os campos do report
            checkReport.IdTipoReport = report.IdTipoReport.Value;
            checkReport.IdFeedbackUsuario = report.IdFeedbackUsuario.Value;
            checkReport.Deferido = report.Deferido;

            await _reportRepository.UpdateReport(checkReport);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var result = await _reportRepository.GetReportByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _reportRepository.DeleteReport(id);

            return NoContent();
        }
    }
}
