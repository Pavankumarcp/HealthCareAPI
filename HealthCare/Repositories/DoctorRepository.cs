using HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Repositories
{
    public class DoctorRepository : IRepository<Doctor>,IGetRepository<Doctor>
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Doctor obj)
        {
            if (obj != null)
            {
                _context.Doctors.Add(obj);
                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public async Task<Doctor> Delete(int id)
        {
            var doct = await _context.Doctors.FindAsync(id);
            if (doct != null)
            {
                _context.Doctors.Remove(doct);
                await _context.SaveChangesAsync();
                return doct;
            }
            return null;
        }

        public async Task<Doctor> Update(int id, Doctor obj)
        {
            var doct = await _context.Doctors.FindAsync(id);
            if (doct != null)
            {
                doct.DoctorName = obj.DoctorName;
                doct.Specialization= obj.Specialization;
                _context.Doctors.Update(doct);
                await _context.SaveChangesAsync();
                return doct;
            }
            return null;
        }

        public async Task<Doctor> GetById(int id)
        {
            var doctors = await _context.Doctors.FindAsync(id);
            if(doctors!=null)
            {
                return doctors;
            }
            return null;
        }
       
    }
}
