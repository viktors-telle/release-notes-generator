﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReleaseNotes.Generator.Dto;
using ReleaseNotes.Generator.Exceptions;

namespace ReleaseNotes.Generator.Controllers
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
            
            return Ok(releasesNotes);
        }
    }
}
