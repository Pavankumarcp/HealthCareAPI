using HealthCare.Models;
using HealthCare.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        //private readonly DoctorRepository _doctorRepository;
        private readonly IGetRepository<Doctor> _doctorgetRepository;
        //private readonly IConfiguration _Configuration;
        //private readonly ApplicationDbContext _dbContext;

        public DoctorController(IRepository<Doctor> repository, IGetRepository<Doctor> doctorgetRepository/*IConfiguration Configuration,ApplicationDbContext applicationDbContext*/)
        {
            _repository = repository;
            //_doctorRepository = doctorRepository;
            _doctorgetRepository = doctorgetRepository;
            //_Configuration = Configuration;
            //_dbContext = applicationDbContext;
        }
        //[HttpPost("Login")]
        //public IActionResult Login([FromBody] Doctor doctor)
        //{
        //    var logu = _dbContext.Doctors.FirstOrDefault(a => a.EmailId == doctor.EmailId);
        //    if (logu == null)
        //    {
        //        return BadRequest("Invalid Username");
        //    }
        //    var logp = _dbContext.Doctors.FirstOrDefault(b => b.Password == doctor.Password);
        //    if (logp == null)
        //    {
        //        return BadRequest("Invalid Password");
        //    }
        //    return Ok();
        //}
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
