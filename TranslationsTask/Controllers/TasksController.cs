using Microsoft.AspNetCore.Mvc;
using TranslationsTask.Dtos;
using TranslationsTask.Services;

namespace TranslationsTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _service;

        public TasksController(ITasksService service)
        {
            _service = service;
        }

        [HttpGet("byProjectId/{id}")]
        public async Task<IActionResult> GetByProjectId([FromRoute] long id, CancellationToken ct)
        {
            return Ok(await _service.GetTasksByProjectIdAsync(id, ct));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id, CancellationToken ct)
        {
            return Ok(await _service.GetTaskAsync(id, ct));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditTaskDto input, CancellationToken ct)
        {
            await _service.UpdateTaskAsync(input, ct);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTaskDto input, CancellationToken ct)
        {
            await _service.AddTaskAsync(input, ct);
            return Ok();
        }

        [HttpGet("translatorsList")]
        public async Task<IActionResult> GetTranslatorsAsync(CancellationToken ct)
        {
            return Ok(await _service.GetTranslatorsAsync(ct));
        }

        [HttpGet("projectsList")]
        public async Task<IActionResult> GetProjectsAsync(CancellationToken ct)
        {
            return Ok(await _service.GetProjectsAsync(ct));
        }
    }
}
