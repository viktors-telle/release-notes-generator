using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Common.Models;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class ReleaseNotesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromBody]ReleaseNotesRequest releaseNotes)
        {
            return Ok("Sample release notes.");
        }
    }
}
