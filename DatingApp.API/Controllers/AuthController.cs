using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // validate request

            userForRegisterDto.username = userForRegisterDto.username.ToLower();

            if(await _repo.UserExists(userForRegisterDto.username))
                return BadRequest("Username already exists!");
            var userToCreate = new User
            {
                Username = userForRegisterDto.username;
            };
            var craetedUser = await _repo.Register(userToCreate, userForRegisterDto.password);

            return StatusCode(201);
        }

    }
}