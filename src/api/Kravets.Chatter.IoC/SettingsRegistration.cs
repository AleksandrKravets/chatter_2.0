using Kravets.Chatter.Common.Settings;
using Kravets.Chatter.IoC.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kravets.Chatter.IoC
{
    public static partial class ServicesRegistration
    {
        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSettings<JwtSettings>(configuration);
            services.ConfigureSettings<PasswordHasherSettings>(configuration);
            services.ConfigureSettings<PasswordSettings>(configuration);
            services.ConfigureSettings<ApplicationSettings>(configuration);
        }
    }
}
