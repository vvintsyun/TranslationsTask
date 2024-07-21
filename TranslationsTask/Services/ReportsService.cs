using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;
using TranslationsTask.Data.TranslationsTask.Data;
using TranslationsTask.Dtos;
using TranslationsTask.Helpers;
using TranslationsTask.Models;

namespace TranslationsTask.Services
{
    public class ReportsService : IReportsService
    {
        private readonly TranslationsContext _translationsContext;

        public ReportsService(TranslationsContext translationsContext)
        {
            _translationsContext = translationsContext;
        }
        public async Task<TasksCountByMonthProjectDto?> GetDataAsync(ReportsFilterDto filters, CancellationToken ct)
        {
            var dataFromSql = await GetSqlData(filters, ct);
            if (dataFromSql.Count == 0)
            {
                return new TasksCountByMonthProjectDto();
            }

            var result = new TasksCountByMonthProjectDto();
            var xValues = dataFromSql.Select(x => new { x.Year, x.Month })
                .DistinctBy(x => new { x.Year, x.Month })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToArray();

            for(int i = 0; i < xValues.Length; i++)
            {
                result.X.Add($"{xValues[i].Month} {xValues[i].Year}");
                foreach (var data in dataFromSql)
                {
                    var projectData = result.Data.FirstOrDefault(xx => xx.Label == data.Project);
                    if (projectData is null)
                    {
                        result.Data.Add(new ChartData { Data = new int[xValues.Length], Label = data.Project });
                        projectData = result.Data[^1];
                    }

                    if (data.Year == xValues[i].Year && data.Month == xValues[i].Month)
                    {
                        projectData.Data[i] = data.Count;
                    }
                }
            }

            return result;
        }
        private async Task<ICollection<ReportDto>> GetSqlData(ReportsFilterDto filters, CancellationToken ct)
        {
            var result = new List<ReportDto>();

            var projectsTvp = new DataTable();
            projectsTvp.Columns.Add(new DataColumn("Id", typeof(long)));

            foreach (var id in filters.Projects)
            {
                projectsTvp.Rows.Add(id); 
            }
            var projectsParam = new SqlParameter("@projects", projectsTvp);
            projectsParam.SqlDbType = SqlDbType.Structured;
            projectsParam.TypeName = "dbo.IdList";

            var translatorsTvp = new DataTable();
            translatorsTvp.Columns.Add(new DataColumn("Id", typeof(long)));
            var includeNullAssigneeParam = new SqlParameter("@includeNullAssigneeParam", SqlDbType.Bit)
            {
                Value = 0
            };

            foreach (var id in filters.Translators)
            {
                if (id == -1)
                {
                    includeNullAssigneeParam.Value = 1;
                    continue;
                }
                translatorsTvp.Rows.Add(id);
            }
            var translatorsParam = new SqlParameter("@translators", translatorsTvp);
            translatorsParam.SqlDbType = SqlDbType.Structured;
            translatorsParam.TypeName = "dbo.IdList";

            await using (var conn = new SqlConnection(_translationsContext.Database.GetConnectionString()))
            {
                var sql = "SELECT * FROM dbo.GetTasksCountByMonthProject(@projects, @translators, @includeNullAssigneeParam)";
                var cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add(projectsParam);
                cmd.Parameters.Add(translatorsParam);
                cmd.Parameters.Add(includeNullAssigneeParam);

                conn.Open();

                var dr = await cmd.ExecuteReaderAsync(ct);

                while (await dr.ReadAsync(ct))
                {
                    result.Add(new ReportDto
                    {
                        Project = dr.GetString(0),
                        Month = dr.GetInt32(1),
                        Year = dr.GetInt32(2),
                        Count = dr.GetInt32(3),
                    });                    
                }

                dr.Close();

                conn.Close();
            }

            return result;
        }
    }
}
