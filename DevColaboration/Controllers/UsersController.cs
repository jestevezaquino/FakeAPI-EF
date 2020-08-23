using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevColaboration.Services;
using DevColaboration.Models.FakeAPI;

namespace DevColaboration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersServices _usersServices;
        private readonly UsersEFServices _usersEFServices;

        public UsersController(UsersServices usersServices, UsersEFServices usersEFServices)
        {
            _usersServices = usersServices;
            _usersEFServices = usersEFServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _usersServices.GetUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ActionName("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _usersServices.GetUserByIdAsync(id);
            if(result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("repository")]
        public IActionResult GetRepository()
        {
            var result = _usersEFServices.GetUsersFromRepository();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] User user)
        {
            var result = await _usersServices.PostUserAsync(user);
            if (result)
                return CreatedAtAction("GetByIdAsync", new { Id = user.Id }, user);
            return BadRequest();
        }
    }
}
