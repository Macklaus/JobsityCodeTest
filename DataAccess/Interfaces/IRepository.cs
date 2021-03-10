using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task CreateAsync(T entity);

        void Update(T entity);
    }
}
