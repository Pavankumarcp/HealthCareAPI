using HealthCare.Models;
using HealthCare.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<Patient> _repository;
        private readonly IGetRepository<Patient> _patientgetRepository;

        public PatientController(IRepository<Patient> repository, IGetRepository<Patient> patientgetRepository)
        {
            _repository = repository;
            _patientgetRepository = patientgetRepository;
        }
        [HttpGet("GetallPatients")]
        public IEnumerable<Patient> GetPatients()
        {
            return _patientgetRepository.GetAll();
        }
        [HttpGet]
        [Route("GetPatientById/{id}", Name = "GetPatientById")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var movie = await _patientgetRepository.GetById(id);
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }
        [HttpPost("CreatePatient")]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(patient);
            return CreatedAtRoute("GetPatientById", new { id = patient.Id }, patient);
        }
        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, patient);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Doctor not found");
        }
        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
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
