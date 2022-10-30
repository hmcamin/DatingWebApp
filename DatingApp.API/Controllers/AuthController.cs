using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.IdentifyModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
		private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
			_config = config;
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
		[HttpPost("login")]
		public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
		{
			var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
			if(userFromRepo == null)
				return Unauthorized();
			
			car claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
				new Claim(ClaimTypes.Name, userFromRepo.Username)
			};
			var key = new SymetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
			
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentify(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};
			
			var tokenHandler = new JwtSecurityTokenHandler();
			
			var token = tokenHandler.CreateToken(tokenDescriptor);
			
			return (new {
				token = tokenHandler.WriteToken(token)
			});
		}

    }
}



























