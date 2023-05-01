using CsvPgApi.Model;
using CsvPgApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CsvPgApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICsvUsersService _csvUsersService;
        public UsersController(ICsvUsersService csvUsersService) {
            _csvUsersService = csvUsersService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsv([FromBody] CsvUploadData csvUploadData)
        {
            var result = await _csvUsersService.ProcessCsv(csvUploadData.Filename, csvUploadData.Content);

            return Ok(result);
        }

        [HttpGet("metadata")]
        public async Task<IActionResult> GetMetadata()
        {
            var result = await _csvUsersService.GetAllCsvs();

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCsvUsers(Guid id)
        {
            var result = await _csvUsersService.GetCsvUsers(id);

            return Ok(result);
        }
    }
}
