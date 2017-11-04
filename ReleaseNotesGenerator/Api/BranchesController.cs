using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Components.Interfaces;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Api
{
    [Route("api/[controller]")]
    public class BranchesController : BaseController
    {        
        private readonly IBranchComponent _branchComponent;

        public BranchesController(IBranchComponent branchComponent)
        {
            _branchComponent = branchComponent;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
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

            var updatedBranch = await _branchComponent.Update(id, branch);
            if (updatedBranch == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
