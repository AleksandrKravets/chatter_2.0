using FluentValidation;
using Kravets.Chatter.API.Models.Authentication;
using Kravets.Chatter.API.Models.Messages;
using Kravets.Chatter.API.Models.SavedMessages;
using Kravets.Chatter.API.Models.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace Kravets.Chatter.API.IoC
{
    public static partial class ServicesRegistration
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateMessageModel>, CreateMessageModel.Validator>();
            services.AddTransient<IValidator<GetMessagesFilter>, GetMessagesFilter.Validator>();
            services.AddTransient<IValidator<UpdateMessageModel>, UpdateMessageModel.Validator>();
            services.AddTransient<IValidator<AuthenticateUserModel>, AuthenticateUserModel.Validator>();
            services.AddTransient<IValidator<RefreshTokensModel>, RefreshTokensModel.Validator>();
            services.AddTransient<IValidator<GetSavedMessagesFilter>, GetSavedMessagesFilter.Validator>();
            services.AddTransient<IValidator<SaveMessageModel>, SaveMessageModel.Validator>();
            services.AddTransient<IValidator<GetLostMessagesFilter>, GetLostMessagesFilter.Validator>();

        }
    }
}
