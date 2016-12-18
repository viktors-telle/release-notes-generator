using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Core;
using ReleaseNotesGenerator.Domain;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IProjectComponent _projectComponent;

        public ProjectsController(IProjectComponent projectComponent)
        {
            _projectComponent = projectComponent;
        }

        [HttpGet("{id}", Name = "GetProjectById")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectComponent.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return new ObjectResult(project);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _projectComponent.Add(project);
            return CreatedAtRoute("GetProjectById", new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Project project)
        {
            if (project == null || id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _projectComponent.Update(id, project);
            if (updatedProject == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
