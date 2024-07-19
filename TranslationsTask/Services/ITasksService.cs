using TranslationsTask.Dtos;

namespace TranslationsTask.Services
{
    public interface ITasksService
    {
        Task AddTaskAsync(AddTaskDto input, CancellationToken ct);
        Task<ICollection<TaskVm>> GetTasksByProjectIdAsync(long projectId, CancellationToken ct);
        Task<ICollection<DropDownEntityDto>> GetTranslatorsAsync(CancellationToken ct);
        Task<ICollection<DropDownEntityDto>> GetProjectsAsync(CancellationToken ct);
        Task<EditTaskDto> GetTaskAsync(long id, CancellationToken ct);
        Task UpdateTaskAsync(EditTaskDto input, CancellationToken ct);
    }
}
