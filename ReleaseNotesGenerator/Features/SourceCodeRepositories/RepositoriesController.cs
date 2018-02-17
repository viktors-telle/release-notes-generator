using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Api;

namespace ReleaseNotesGenerator.Features.SourceCodeRepositories
{
    [Route("api/[controller]")]
    public class RepositoriesController : BaseController
    {        
        private readonly IRepositoryComponent _repositoryComponent;

        public RepositoriesController(IRepositoryComponent projectComponent)
        {
            _repositoryComponent = projectComponent;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Repository>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjects()
        {
            var repositories = await _repositoryComponent.GetRepositories();
            return Ok(repositories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var project = await _repositoryComponent.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return new ObjectResult(project);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Repository repository)
        {
            if (repository == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   

            await _repositoryComponent.Add(repository);
            return new ObjectResult(repository);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Repository repository)
        {            
            if (repository == null || id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedRepository = await _repositoryComponent.Update(id, repository);
            if (updatedRepository == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpGet("{repositoryId}/releaseNotes")]
        public async Task<IActionResult> GetRepositoryReleaseNotes(int repositoryId)
        {
            var releaseNotes = await _repositoryComponent.GetRepositoryReleaseNotes(repositoryId);
            return Ok(releaseNotes);
        }
    }
}
