using Microsoft.AspNetCore.Mvc;

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class ReleaseNotesController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Sample release notes";
        }
    }
}
