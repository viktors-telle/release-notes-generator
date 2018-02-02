using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Features.Projects
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IProjectComponent _projectComponent;

        public ProjectsController(IProjectComponent projectComponent)
        {
            _projectComponent = projectComponent;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Project>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectComponent.GetProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectComponent.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
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
            return Ok(project);
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
