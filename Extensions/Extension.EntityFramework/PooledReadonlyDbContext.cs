using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Extension.EntityFramework
{
    public class PooledReadonlyDbContext<TContext> : IPooledReadonlyDbContext<TContext>
        where TContext : DbContext
    {
        private readonly IDbContextFactory<TContext> _contextFactory;

        public PooledReadonlyDbContext(IDbContextFactory<TContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<T> QueryAsync<T>(Func<TContext, Task<T>> queryFunc)
        {
            await using var context = _contextFactory.CreateDbContext();
            return await queryFunc(context);
        }
    }
}