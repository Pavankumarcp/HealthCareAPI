using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCare.Repositories
{
    public interface IGetRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
    }
    public interface IRepository<T> where T: class 
    {
        Task Create(T obj);

        Task<T> Update(int id, T obj);

        Task<T> Delete(int id);
    }
}
