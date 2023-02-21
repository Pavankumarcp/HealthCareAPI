using HealthCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly ApplicationDbContext _dbContext;
        public AccountsController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _Configuration = configuration;
            _dbContext = dbContext;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPatient(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            //user.Role = "Customer";
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("RegisterDoctor")]
        public async Task<IActionResult> RegisterDoctor(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.EmailId == login.EmailId && x.Password == login.Password);
            if (currentUser == null)
            {
                return NotFound("Invalid UserName  or  Password");
            }
            var token = GenerateToken(currentUser);
            if (token == null)
            {
                return NotFound("Invalid credentials");
            }


            return Ok(token);
        }
        [NonAction]
        public string GenerateToken(User user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Secretkey"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha512);
            var myclaims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,user.EmailId),

            };
            var token = new JwtSecurityToken(issuer: _Configuration["JWT:issuer"],

                                            claims: myclaims,
                                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        //[HttpGet("GetRole")] 
        //public IActionResult GetRole() 
        //{ 
        //   var Role=ClaimTypes.Role; 
        //    return Ok(Role); 
        //}

        [HttpGet("GetRole")]
        public IActionResult GetRole()
        {
            var Role = User.FindFirstValue(ClaimTypes.Role);
            return Ok(Role);
        }
        [HttpGet("GetName")] 
        public IActionResult GetName() 
        {
            var login = User.FindFirstValue(ClaimTypes.Name);
            return Ok(login); 
        }
    }

}
