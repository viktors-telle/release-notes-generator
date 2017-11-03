using Microsoft.AspNetCore.Mvc;

namespace ReleaseNotes.Generator.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Error()
        {            
            return View();
        }
    }
}
