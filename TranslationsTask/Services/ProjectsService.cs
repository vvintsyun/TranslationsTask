using Microsoft.EntityFrameworkCore;
using TranslationsTask.Data.TranslationsTask.Data;
using TranslationsTask.Dtos;
using TranslationsTask.Helpers;
using TranslationsTask.Models;

namespace TranslationsTask.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly TranslationsContext _translationsContext;

        public ProjectsService(TranslationsContext translationsContext)
        {
            _translationsContext = translationsContext;
        }

        public async Task AddProjectAsync(AddProjectDto input, CancellationToken ct)
        {
            var isExist = await _translationsContext.Projects.AnyAsync(x => x.Name == input.Name);

            if (isExist)
            {
                throw new UserFriendlyException("Project already exists");
            }

            _translationsContext.Add(new TranslationProject(input.Name));
            await _translationsContext.SaveChangesAsync(ct);
        }        

        public async Task<ICollection<ProjectVm>> GetProjectsAsync(CancellationToken ct)
        {
            return await _translationsContext.Projects
                .Select(x => new ProjectVm 
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(ct);
        }
    }
}
