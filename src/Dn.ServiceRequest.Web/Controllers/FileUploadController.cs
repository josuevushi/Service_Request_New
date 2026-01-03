using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Dn.ServiceRequest.PieceJointes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Volo.Abp.AspNetCore.Mvc;

namespace Dn.ServiceRequest.Web.Controllers
{
    [Route("api/files")]
    public class FileUploadController : AbpController
    {
        private readonly IConfiguration _configuration;

        public FileUploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Upload([FromBody] FileDto input)
        {
            if (!ModelState.IsValid)
            {
                 var errors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest("Validation failed: " + errors);
            }

            if (input == null || string.IsNullOrWhiteSpace(input.Base64) || string.IsNullOrWhiteSpace(input.FileName))
            {
                return BadRequest("Invalid file data. FileName and Base64 content are required.");
            }

            try
            {
                var uploadPath = _configuration["FileSettings:UploadPath"];
                if (string.IsNullOrWhiteSpace(uploadPath))
                {
                    return StatusCode(500, "Upload path is not configured in the server settings.");
                }

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Ensure the file name is safe
                var fileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(input.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                var fileBytes = Convert.FromBase64String(input.Base64);
                
                await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

                return Ok(new { message = "File uploaded successfully", filePath = filePath });
            }
            catch (FormatException)
            {
                return BadRequest("Invalid Base64 string.");
            }
            catch (Exception ex)
            {
                // In a real scenario, use ILogger to log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete")]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete([FromQuery] string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return BadRequest("File path is required.");
            }

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                System.IO.File.Delete(filePath);
                return Ok(new { message = "File deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return BadRequest("File path is required.");
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "application/octet-stream";
            var fileName = Path.GetFileName(filePath);

            return File(memory, contentType, fileName);
        }
    }
}
