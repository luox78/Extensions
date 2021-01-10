using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Extension.EntityFramework
{
    public class PooledReadonlyDbContext<TContext> : IPooledReadonlyDbContext<TContext>
        where TContext : DbContext
    {
        private readonly DbContextPool<TContext> _pool;

        public PooledReadonlyDbContext(DbContextPool<TContext> pool)
        {
            _pool = pool;
        }


        public async Task<T> QueryAsync<T>(Func<TContext, Task<T>> queryFunc)
        {
            var context = _pool.Rent();
            var result = default(T);

            try
            {
                result = await queryFunc(context);
                _pool.Return(context);
            }
            catch
            {
                await context.DisposeAsync();
            }

            return result;
        }
    }
}