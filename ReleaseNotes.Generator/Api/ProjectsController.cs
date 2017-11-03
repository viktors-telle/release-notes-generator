using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Api
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IProjectComponent _projectComponent;

        public ProjectsController(IProjectComponent projectComponent)
        {
            _projectComponent = projectComponent;
        }

        [HttpGet("{id}")]
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

            await _projectComponent.Add(project);
            return new ObjectResult(project);
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
