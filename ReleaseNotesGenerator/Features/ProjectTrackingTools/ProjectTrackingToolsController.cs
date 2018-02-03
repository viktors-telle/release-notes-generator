using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReleaseNotesGenerator.Features.ProjectTrackingTools
{
    [Route("api/[controller]")]
    public class ProjectTrackingToolsController : Controller
    {        
        private readonly IProjectTrackingToolComponent _projectTrackingToolComponent;

        public ProjectTrackingToolsController(IProjectTrackingToolComponent projectTrackingToolComponent)
        {
            _projectTrackingToolComponent = projectTrackingToolComponent;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var projectTrackingTool = await _projectTrackingToolComponent.GetById(id);
            if (projectTrackingTool == null)
            {
                return NotFound();
            }

            return new ObjectResult(projectTrackingTool);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProjectTrackingTool projectTrackingTool)
        {
            if (projectTrackingTool == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _projectTrackingToolComponent.Add(projectTrackingTool);
            return new ObjectResult(projectTrackingTool);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]ProjectTrackingTool projectTrackingTool)
        {
            if (projectTrackingTool == null || id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTrackingTool = await _projectTrackingToolComponent.Update(id, projectTrackingTool);
            if (updatedTrackingTool == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
