using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Repository
{
    public class TipoReportRepository : ITipoReportRepository
    {
        private readonly SampleContext _dbContext;

        public TipoReportRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<TipoReport>> GetTipoReport()
        {
            return await _dbContext.TipoReport.ToListAsync();
        }

        public async Task<TipoReport> GetTipoReportByIdAsync(int id)
        {
            return await _dbContext.TipoReport.FindAsync(id);
        }

        public async Task AddTipoReport(TipoReport tipoReport)
        {
            await _dbContext.TipoReport.AddAsync(tipoReport);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTipoReport(TipoReport tipoReport)
        {
            _dbContext.TipoReport.Update(tipoReport);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTipoReport(int id)
        {
            var tipoReport = await GetTipoReportByIdAsync(id);
            if (tipoReport != null)
            {
                _dbContext.TipoReport.Remove(tipoReport);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
