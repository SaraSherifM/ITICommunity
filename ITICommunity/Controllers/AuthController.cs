using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITICommunity.DTOs;
using ITICommunity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace ITICommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration configuration;

        public AuthController(IAuthRepository _repo, IConfiguration _configuration)
        {
            repo = _repo;
            configuration = _configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.emailaddress = userForRegisterDto.emailaddress.ToLower();
            if (await repo.UserExists(userForRegisterDto.emailaddress))
                return BadRequest("User Aleardy Exists");
            var userToCreate = new User
            {
                Email = userForRegisterDto.emailaddress,
                
            };

            var createdUser = repo.Register(userToCreate, userForRegisterDto.password);

            return CreatedAtAction("GetUsers", createdUser);

        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn (UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await repo.LogIn(userForLoginDto.emailaddress.ToLower(), userForLoginDto.password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim (ClaimTypes.Email, userFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credintials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }
    }
}