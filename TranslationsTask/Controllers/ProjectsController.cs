using Microsoft.AspNetCore.Mvc;
using TranslationsTask.Dtos;
using TranslationsTask.Services;

namespace TranslationsTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _service;

        public ProjectsController(IProjectsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            return Ok(await _service.GetProjectsAsync(ct));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddProjectDto input, CancellationToken ct)
        {
            await _service.AddProjectAsync(input, ct);
            return Ok();
        }
    }
}