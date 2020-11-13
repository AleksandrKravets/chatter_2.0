using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kravets.Chatter.DAL.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStoredProcedureExecutor(this IServiceCollection services, Action<DatabaseSettings> settings)
        {
            services.AddTransient<StoredProcedureExecutor>();
            services.Configure(settings);
        }
    }
}