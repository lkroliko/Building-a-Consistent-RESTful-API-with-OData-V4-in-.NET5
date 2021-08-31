using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirVinyl.SharedKernel.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> AsQueryable<T>() where T : BaseEntity;
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task AddAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity, T values) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task<bool> AnyAsync<T>(int id) where T : BaseEntity;
        Task DeleteRangeAsync<T>(IEnumerable<T> entitys) where T : BaseEntity;
    }
}
