using System.Threading.Tasks;
using DocumentService.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DocumentsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Validate(Document document)
        {
            return Ok();
        }
    }
}
