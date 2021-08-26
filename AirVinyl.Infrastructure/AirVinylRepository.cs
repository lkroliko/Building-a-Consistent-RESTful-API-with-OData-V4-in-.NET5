using AirVinyl.SharedKernel;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AirVinyl.Infrastructure
{
    public class AirVinylRepository : IRepository
    {
        private readonly AirVinylDbContext _context;

        public AirVinylRepository(AirVinylDbContext context) 
        {
            _context = context;
        }

        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable AsQueryable<T>() where T : BaseEntity
        {
            return _context.Set<T>();
        }

        public Task AddAsync<T>(T entity) where T : BaseEntity
        {
            _context.Add<T>(entity);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync<T>(T entity, T values) where T: BaseEntity
        {
            _context.Update<T>(entity).CurrentValues.SetValues(values);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync<T>(T entity) where T: BaseEntity
        {
            _context.Remove(entity);
            return _context.SaveChangesAsync();
        }
    }
}
