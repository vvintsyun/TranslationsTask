using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TranslationsTask.Data.TranslationsTask.Data;
using TranslationsTask.Dtos;
using TranslationsTask.Helpers;
using TranslationsTask.Models;

namespace TranslationsTask.Services
{
    public class TasksService : ITasksService
    {
        private readonly TranslationsContext _translationsContext;

        public TasksService(TranslationsContext translationsContext)
        {
            _translationsContext = translationsContext;
        }

        public async Task<ICollection<TaskVm>> GetTasksByProjectIdAsync(long projectId, CancellationToken ct)
        {
            return await _translationsContext.Tasks
                .Where(x => x.ProjectId == projectId)
                .Select(x => new TaskVm
                {
                    Id = x.Id,
                    Description = x.Description,
                    Project = x.Project.Name,
                    DeadlineValue = x.Deadline,
                    Assignee = x.Assignee == null 
                        ? null
                        : x.Assignee.FullName,
                    Title = x.Title
                })
                .ToListAsync(ct);
        }

        public async Task<EditTaskDto> GetTaskAsync(long taskId, CancellationToken ct)
        {
            return await _translationsContext.Tasks
                .Where(x => x.Id == taskId)
                .Select(x => new EditTaskDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    ProjectId = x.ProjectId,
                    Deadline = x.Deadline.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    AssigneeId = x.AssigneeId,
                    Title = x.Title
                })
                .FirstOrDefaultAsync(ct);
        }
        public async Task AddTaskAsync(AddTaskDto input, CancellationToken ct)
        {
            DateTime deadline;
            if (!DateTime.TryParseExact(input.Deadline, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out deadline))
            {
                throw new UserFriendlyException("Incorrect deadline date");
            }
            var task = new TranslationTask(input.Title, input.Description, deadline, input.ProjectId, input.AssigneeId);
            _translationsContext.Add(task);
            await _translationsContext.SaveChangesAsync(ct);
        }

        public async Task UpdateTaskAsync(EditTaskDto input, CancellationToken ct)
        {
            var task = await _translationsContext.Tasks
                .FirstOrDefaultAsync(x => x.Id == input.Id, ct);

            if (task is null)
            {
                throw new UserFriendlyException("Task doesn't exist");
            }

            task.UpdateTask(input);
            await _translationsContext.SaveChangesAsync(ct);
        }

        public async Task<ICollection<DropDownEntityDto>> GetTranslatorsAsync(CancellationToken ct)
        {
            return await _translationsContext.Translators
                .Select(x => new DropDownEntityDto
                {
                    Value = x.FullName,
                    Key = x.Id
                })
                .ToListAsync(ct);
        }

        public async Task<ICollection<DropDownEntityDto>> GetProjectsAsync(CancellationToken ct)
        {
            return await _translationsContext.Projects
                .Select(x => new DropDownEntityDto
                {
                    Key = x.Id,
                    Value = x.Name
                })
                .ToListAsync(ct);
        }
    }
}
