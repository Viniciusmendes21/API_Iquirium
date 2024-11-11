using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
{
    public interface ITipoReportRepository
    {
        Task<IEnumerable<TipoReport>> GetTipoReport();
        Task<TipoReport> GetTipoReportByIdAsync(int id);
        Task AddTipoReport(TipoReport tipoReport);
        Task UpdateTipoReport(TipoReport tipoReport);
        Task DeleteTipoReport(int id);
    }
}
