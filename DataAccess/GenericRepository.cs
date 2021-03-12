using Microsoft.EntityFrameworkCore;
using Model.DataContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly ChatDbContext _context;

        public GenericRepository(ChatDbContext context)
        {
            _context = context;
        }

        public virtual async Task CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
            } catch (Exception)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
