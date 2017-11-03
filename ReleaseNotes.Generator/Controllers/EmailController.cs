﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailComponent _emailComponent;

        public EmailController(IEmailComponent emailComponent)
        {
            _emailComponent = emailComponent;
        }

        [HttpGet]        
        public async Task<IActionResult> Get([FromQuery]EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _emailComponent.Send(emailRequest);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _emailComponent.Send(emailRequest);
            return Ok();
        }
    }
}
