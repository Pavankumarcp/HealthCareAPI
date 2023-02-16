using HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Repositories
{
    public class PatientRepository : IRepository<Patient>,IGetRepository<Patient>
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Patient obj)
        {
            if (obj != null)
            {
                _context.Patients.Add(obj);
                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }


        public async Task<Patient> Delete(int id)
        {
            var patientsDb = await _context.Patients.FindAsync(id);
            if (patientsDb != null)
            {
                _context.Patients.Remove(patientsDb);
                await _context.SaveChangesAsync();
                return patientsDb;
            }
            return null;
        }

        public async Task<Patient> Update(int id, Patient obj)
        {
            var patientsDb = await _context.Patients.FindAsync(id);
            if (patientsDb != null)
            {
                patientsDb.Age = obj.Age;
                patientsDb.Spec = obj.Spec;
                patientsDb.SlotDate = obj.SlotDate;
                patientsDb.MobileNo = obj.MobileNo;
                _context.Patients.Update(patientsDb);
                await _context.SaveChangesAsync();
                return patientsDb;
            }
            return null;
        }

        public async Task<Patient> GetById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                return patient;
            }
            return null;
        }
    }
}
