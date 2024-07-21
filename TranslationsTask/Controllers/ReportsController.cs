using Microsoft.AspNetCore.Mvc;
using TranslationsTask.Dtos;
using TranslationsTask.Services;

namespace TranslationsTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _service;

        public ReportsController(IReportsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ReportsFilterDto filters, CancellationToken ct)
        {
            return Ok(await _service.GetDataAsync(filters, ct));
        }
    }
}