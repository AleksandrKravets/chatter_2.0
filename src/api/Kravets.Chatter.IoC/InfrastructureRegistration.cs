using Kravets.Chatter.BLL.Contracts.Factories;
using Kravets.Chatter.BLL.Contracts.Hashers;
using Kravets.Chatter.BLL.Contracts.Services;
using Kravets.Chatter.BLL.Contracts.Validators;
using Kravets.Chatter.BLL.Factories;
using Kravets.Chatter.BLL.Hashers;
using Kravets.Chatter.BLL.Services;
using Kravets.Chatter.BLL.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Kravets.Chatter.IoC
{
    public static partial class ServicesRegistration
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokensFactory, TokensFactory>();
            services.AddTransient<IJwtTokensFactory, JwtTokensFactory>();
            services.AddSingleton(typeof(IOnlineUsersService), typeof(OnlineUsersService));

        }
    }
}
