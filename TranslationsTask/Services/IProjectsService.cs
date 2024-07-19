using TranslationsTask.Dtos;

namespace TranslationsTask.Services
{
    public interface IProjectsService
    {
        Task AddProjectAsync(AddProjectDto input, CancellationToken ct);
        Task<ICollection<ProjectVm>> GetProjectsAsync(CancellationToken ct);
    }
}
