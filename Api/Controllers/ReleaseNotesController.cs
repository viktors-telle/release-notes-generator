using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Common.Models;
using ReleaseNotesGenerator.Core;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class ReleaseNotesController : Controller
    {
        private readonly IReleaseNotesComponent _releaseNotesComponent;

        public ReleaseNotesController(IReleaseNotesComponent releaseNotesComponent)
        {
            _releaseNotesComponent = releaseNotesComponent;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ReleaseNotesRequest releaseNotesRequest)
        {
            var releasesNotes = await _releaseNotesComponent.Get(releaseNotesRequest);
            return Ok(releasesNotes);
        }
    }
}
