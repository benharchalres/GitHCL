using DocumentApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DocumentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateDocument([FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest(new
                {
                    Errors = new[] { "Request body cannot be null" }
                });
            }

            // Call validation logic from the model
            var errors = document.Validate();

            if (errors.Any())
            {
                // Invalid → return 400 with error messages
                return BadRequest(new { Errors = errors });
            }

            // Valid → return 200 OK
            return Ok(new
            {
                Message = "Document is valid"
            });
        }
    }
}