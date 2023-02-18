using HealthCare.Models;
using HealthCare.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        //private readonly DoctorRepository _doctorRepository;
        private readonly IGetRepository<Doctor> _doctorgetRepository;
        private readonly IConfiguration _Configuration;
        private readonly ApplicationDbContext _dbContext;

        public DoctorController(IRepository<Doctor> repository, IGetRepository<Doctor> doctorgetRepository, IConfiguration Configuration, ApplicationDbContext applicationDbContext)
        {
            _repository = repository;
            //_doctorRepository = doctorRepository;
            _doctorgetRepository = doctorgetRepository;
            _Configuration = Configuration;
            _dbContext = applicationDbContext;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Doctor doctor)
        {
            var logu = _dbContext.Doctors.FirstOrDefault(a => a.EmailId == doctor.EmailId && a.Password == doctor.Password);
            if (logu == null)
            {
                return BadRequest("Invalid Username or Password");
            }
           var token=GenerateToken(logu);
            if(token== null)
            {
                return NotFound("Invalid Credentials");
            }
            return Ok(token);
        }
        [NonAction]
        public string GenerateToken(Doctor doctor)
        {
            var securitkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securitkey, SecurityAlgorithms.HmacSha512);
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,doctor.DoctorName),
                new Claim(ClaimTypes.Email,doctor.EmailId),
                //  new Claim(ClaimTypes.Role,doctor.Role),
            };
            var token = new JwtSecurityToken(issuer: _Configuration["JWT:issuer"], audience: _Configuration["JWT:audience"],claims:claim,expires:DateTime.Now.AddDays(1),signingCredentials:credentials);
            var tokens=new JwtSecurityTokenHandler().WriteToken(token);
            return tokens;

        }
        [HttpGet("GetallDoctors")]
        public IEnumerable<Doctor> GetDoctors()
        {
            return _doctorgetRepository.GetAll();
        }
        [HttpGet]
        [Route("GetDoctorById/{id}", Name = "GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var movie = await _doctorgetRepository.GetById(id);
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }
        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody]Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(doctor);
            return CreatedAtRoute("GetDoctorById", new { id = doctor.Id },doctor);
        }
        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, doctor);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Doctor not found");
        }
        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Doctor not found");
        }
    }
}
