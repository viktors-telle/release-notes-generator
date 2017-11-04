using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Components.Interfaces;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Api
{
    [Route("api/[controller]")]
    public class RepositoryItemPathsController : BaseController
    {        
        private readonly IRepositoryItemPathComponent _repositoryItemPathComponent;

        public RepositoryItemPathsController(IRepositoryItemPathComponent repositoryItemPathComponent)
        {
            _repositoryItemPathComponent = repositoryItemPathComponent;            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
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

            var updatedBranch = await _repositoryItemPathComponent.Update(id, repositoryItemPath);
            if (updatedBranch == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
