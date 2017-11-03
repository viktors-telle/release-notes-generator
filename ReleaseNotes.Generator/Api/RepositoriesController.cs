using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Components.Interfaces.Authorization;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Api
{
    [Route("api/[controller]")]
    public class RepositoriesController : BaseController
    {        
        private readonly IRepositoryComponent _repositoryComponent;
        private readonly IRepositoryAuthorizationComponent _repositoryAuthorizationComponent;

        public RepositoriesController(IRepositoryComponent projectComponent, IRepositoryAuthorizationComponent repositoryAuthorizationComponent)
        {
            _repositoryComponent = projectComponent;
            _repositoryAuthorizationComponent = repositoryAuthorizationComponent;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if (! await  _repositoryAuthorizationComponent.IsAuthorizedToGetOrUpdate(
                GetAuthorizationParameters(Request.Headers), id))
            {
                return Unauthorized();
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

            if (!await _repositoryAuthorizationComponent.IsAuthorizedToAdd(
                GetAuthorizationParameters(Request.Headers), repository.ProjectId))
            {
                return Unauthorized();
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

            if (!await _repositoryAuthorizationComponent.IsAuthorizedToGetOrUpdate(
                GetAuthorizationParameters(Request.Headers), id))
            {
                return Unauthorized();
            }

            var updatedRepository = await _repositoryComponent.Update(id, repository);
            if (updatedRepository == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
