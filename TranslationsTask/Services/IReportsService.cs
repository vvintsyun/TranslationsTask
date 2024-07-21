
using TranslationsTask.Dtos;

namespace TranslationsTask.Services
{
    public interface IReportsService
    {
        Task<TasksCountByMonthProjectDto?> GetDataAsync(ReportsFilterDto filters, CancellationToken ct);
    }
}
