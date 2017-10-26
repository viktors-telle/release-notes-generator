using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Components.Interfaces.Authorization;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Controllers
{
    [Route("api/[controller]")]
    public class RepositoryItemPathsController : BaseController
    {        
        private readonly IRepositoryItemPathComponent _repositoryItemPathComponent;
        private readonly IRepositoryItemPathAuthorizationComponent _repositoryItemPathAuthorizationComponent;

        public RepositoryItemPathsController(IRepositoryItemPathComponent repositoryItemPathComponent, 
            IRepositoryItemPathAuthorizationComponent repositoryItemPathAuthorizationComponent)
        {
            _repositoryItemPathComponent = repositoryItemPathComponent;
            _repositoryItemPathAuthorizationComponent = repositoryItemPathAuthorizationComponent;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if (!await _repositoryItemPathAuthorizationComponent.IsAuthorizedToGetOrUpdate(
                GetAuthorizationParameters(Request.Headers), id))
            {
                return Forbid();
            }

            var branch = await _repositoryItemPathComponent.GetById(id);
            if (branch == null)
            {
                return NotFound();
            }

            return new ObjectResult(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]RepositoryItemPath repositoryItemPath)
        {
            if (repositoryItemPath == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repositoryItemPathAuthorizationComponent.IsAuthorizedToAdd(
                GetAuthorizationParameters(Request.Headers), repositoryItemPath.BranchId))
            {
                return Forbid();
            }

            await _repositoryItemPathComponent.Add(repositoryItemPath);
            return new ObjectResult(repositoryItemPath);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]RepositoryItemPath repositoryItemPath)
        {
            if (repositoryItemPath == null || id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repositoryItemPathAuthorizationComponent.IsAuthorizedToGetOrUpdate(
                GetAuthorizationParameters(Request.Headers), repositoryItemPath.Id))
            {
                return Forbid();
            }

            var updatedBranch = await _repositoryItemPathComponent.Update(id, repositoryItemPath);
            if (updatedBranch == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
