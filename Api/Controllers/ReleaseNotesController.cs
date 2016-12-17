using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class ReleaseNotesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "Sample release notes";
        }
    }
}
