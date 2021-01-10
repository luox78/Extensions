using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Extension.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IRepository<TContext> where TContext : DbContext
    {
        Task<T> GetAsync<T>(object id) where T : class;

        Task<T> AddAsync<T>(T entity) where T : class;

        Task UpdateAsync<T>(T entity) where T : class;

        Task DeleteAsync<T>(T entity) where T : class;

        Task<List<T>> QueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;

        IQueryable<T> GetQueryable<T>() where T : class;

        Task<int> SaveChangesAsync();
    }
}