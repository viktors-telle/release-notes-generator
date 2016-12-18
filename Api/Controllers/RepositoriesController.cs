using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Domain;
using ReleaseNotesGenerator.Core;

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class RepositoriesController : Controller
    {
        private readonly IRepositoryComponent _repositoryComponent;

        public RepositoriesController(IRepositoryComponent projectComponent)
        {
            _repositoryComponent = projectComponent;
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> Get(int id)
        {
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

            var id = await _repositoryComponent.Add(repository);
            return CreatedAtRoute("GetById", new { id }, null);
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
    }
}
