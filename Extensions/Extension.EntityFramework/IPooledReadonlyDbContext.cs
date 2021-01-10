using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Extension.EntityFramework
{
    public interface IPooledReadonlyDbContext<out TContext> where TContext : DbContext
    {
        Task<T> QueryAsync<T>(Func<TContext, Task<T>> queryFunc);
    }
}