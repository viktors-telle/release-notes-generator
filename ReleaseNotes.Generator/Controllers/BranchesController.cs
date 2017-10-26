using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Components.Interfaces.Authorization;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Controllers
{
    [Route("api/[controller]")]
    public class BranchesController : BaseController
    {        
        private readonly IBranchComponent _branchComponent;
        private readonly IBranchAuthorizationComponent _branchAuthorizationComponent;

        public BranchesController(IBranchComponent branchComponent, IBranchAuthorizationComponent branchAuthorizationComponent)
        {
            _branchComponent = branchComponent;
            _branchAuthorizationComponent = branchAuthorizationComponent;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if (!await _branchAuthorizationComponent.IsAuthorizedToGet(
               GetAuthorizationParameters(Request.Headers), id))
            {
                return Unauthorized();
            }

            var branch = await _branchComponent.GetById(id);
            if (branch == null)
            {
                return NotFound();
            }

            return new ObjectResult(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Branch branch)
        {
            if (branch == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _branchAuthorizationComponent.IsAuthorizedToAdd(
                GetAuthorizationParameters(Request.Headers), branch.RepositoryId))
            {
                return Unauthorized();
            }

            await _branchComponent.Add(branch);
            return new ObjectResult(branch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Branch branch)
        {
            if (branch == null || id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _branchAuthorizationComponent.IsAuthorizedToUpdate(
                GetAuthorizationParameters(Request.Headers), branch.RepositoryId, id))
            {
                return Unauthorized();
            }

            var updatedBranch = await _branchComponent.Update(id, branch);
            if (updatedBranch == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
