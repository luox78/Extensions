using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Extension.EntityFramework
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEntityFrameworkExtension(this IServiceCollection sc)
        {
            return sc.AddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
        }

        public static IServiceCollection AddReadonlyDbContextPool<TContext>(
            this IServiceCollection sc,
            Action<IServiceProvider, DbContextOptionsBuilder> optionAction, 
            int poolSize = 32)
            where TContext : DbContext
        {
            sc.AddPooledDbContextFactory<TContext>(optionAction, poolSize);
            sc.TryAddSingleton(typeof(IPooledReadonlyDbContext<>), typeof(PooledReadonlyDbContext<>));

            return sc;
        }

        /*private static readonly ILoggerFactory DebugUseLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug(); });*/

        public static DbContextOptions<TContext> QuickCreateDbContextOptions<TContext>(IServiceProvider serviceProvider) 
            where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();

            /*builder.ConfigureWarnings(w => w.Log(CoreEventId.ManyServiceProvidersCreatedWarning));
            builder.UseSqlServer(connectionString, option => option.EnableRetryOnFailure());*/

            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
#if DEBUG
            //builder.UseLoggerFactory(DebugUseLoggerFactory);
            builder.EnableSensitiveDataLogging();
#endif

            return builder.Options;
        }
    }
}