using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kravets.Chatter.IoC.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureSettings<TOptions>(this IServiceCollection services, IConfiguration configuration) where TOptions : class
        {
            string sectionName = typeof(TOptions).Name;
            IConfigurationSection section = configuration.GetSection(sectionName);
            services.Configure<TOptions>(section);
        }
    }
}
