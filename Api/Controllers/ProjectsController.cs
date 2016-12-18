using Microsoft.AspNetCore.Mvc;

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Add([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody]string value)
        {
        }

    }
}
