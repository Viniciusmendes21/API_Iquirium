using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly SampleContext _dbContext;

        public ReportRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Report>> GetReport()
        {
            return await _dbContext.Report.ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await _dbContext.Report.FindAsync(id);
        }

        public async Task AddReport(Report report)
        {
            await _dbContext.Report.AddAsync(report);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateReport(Report report)
        {
            _dbContext.Report.Update(report);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReport(int id)
        {
            var Report = await GetReportByIdAsync(id);
            if (Report != null)
            {
                _dbContext.Report.Remove(Report);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
