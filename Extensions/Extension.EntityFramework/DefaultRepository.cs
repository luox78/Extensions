using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Extension.EntityFramework
{
    public class DefaultRepository<TContext> : IRepository<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public DefaultRepository(TContext context)
        {
            _context = context;
        }

        public async Task<T> GetAsync<T>(object id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            return (await _context.AddAsync(entity)).Entity;
        }

        public Task UpdateAsync<T>(T entity) where T : class
        {
            return Task.FromResult(_context.Update(entity));
        }

        public Task DeleteAsync<T>(T entity) where T : class
        {
            return Task.FromResult(_context.Remove(entity));
        }

        public Task<List<T>> QueryAsync<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null)
            {
                return _context.Set<T>().ToListAsync();
            }
            return _context.Set<T>().Where(filter).ToListAsync();
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            return _context.Set<T>();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}