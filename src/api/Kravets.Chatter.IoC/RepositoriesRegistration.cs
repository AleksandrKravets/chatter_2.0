using Kravets.Chatter.DAL.Contracts.Repositories;
using Kravets.Chatter.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Kravets.Chatter.IoC
{
    public static partial class ServicesRegistration
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMessagesRepository, MessagesRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITokensRepository, TokensRepository>();
            services.AddTransient<IMutesRepository, MutesRepository>();
            services.AddTransient<ISavedMessagesRepository, SavedMessagesRepository>();
        }
    }
}