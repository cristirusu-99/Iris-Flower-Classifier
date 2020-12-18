using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() => _repository.GetAll().ToList();

        [HttpGet("{id}", Name = "GetById")]

        public ActionResult<User> GetById(int id) => _repository.GetById(id);

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user)
        {
            _repository.Register(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        [HttpPost]
        [Route("login")]
        public ObjectResult Login(User user)
        {
            var result = _repository.Login(user);
            if(result == true)
            {
                string securityKey = "this_is_super_long_security_key_for_token_validation_project_2018_09_07$smesk.in";

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                claims.Add(new Claim(ClaimTypes.Role, "Reader"));
                claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
                claims.Add(new Claim("Id", "110"));

                var token = new JwtSecurityToken(
                    issuer: "smesk.in",
                    audience: "readers",
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredentials,
                    claims: claims
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return Unauthorized("Invalid credentials. Couldn't issue JWT.");
            }
        }
    }
}