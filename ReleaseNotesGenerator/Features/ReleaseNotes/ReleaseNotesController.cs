using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReleaseNotesGenerator.Exceptions;

namespace ReleaseNotesGenerator.Features.ReleaseNotes
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
        [Route("generate")]
        public async Task<IActionResult> Generate([FromQuery]ReleaseNotesRequest releaseNotesRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var releasesNotes = string.Empty;
            try
            {
                releasesNotes = await _releaseNotesComponent.Get(releaseNotesRequest);
            }
            catch (CommitsNotFoundException ex)
            {
                Serilog.Log.Warning(ex, $"Commits not found. Release notes request: {JsonConvert.SerializeObject(releaseNotesRequest)}");
                return Ok(releasesNotes);
            }
            catch (RelatedWorkItemsNotFoundException ex)
            {
                Serilog.Log.Warning(ex, $"Related work items not found. Release notes request: {JsonConvert.SerializeObject(releaseNotesRequest)}");
                return Ok(releasesNotes);
            }

            return Ok(new { releasesNotes });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var releasesNotes = await _releaseNotesComponent.GetAll();
            return Ok(releasesNotes);
        }
    }
}
