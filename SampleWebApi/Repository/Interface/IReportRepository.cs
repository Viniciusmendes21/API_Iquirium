using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetReport();
        Task<Report> GetReportByIdAsync(int id);
        Task AddReport(Report report);
        Task UpdateReport(Report report);
        Task DeleteReport(int id);
    }
}
