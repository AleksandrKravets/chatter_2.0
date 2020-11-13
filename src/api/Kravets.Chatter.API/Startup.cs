using FluentValidation.AspNetCore;
using Kravets.Chatter.API.BackgroundServices;
using Kravets.Chatter.API.BackgroundServices.Models.DelayedMessageDeletion;
using Kravets.Chatter.API.Hubs;
using Kravets.Chatter.API.IoC;
using Kravets.Chatter.API.Middleware;
using Kravets.Chatter.BLL.Commands.Accounts;
using Kravets.Chatter.BLL.Helpers;
using Kravets.Chatter.Common.Constants;
using Kravets.Chatter.DAL.Infrastructure.Extensions;
using Kravets.Chatter.IoC;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Kravets.Chatter.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterRepositories();
            services.RegisterValidators();
            services.RegisterInfrastructure();
            services.RegisterSettings(_configuration);

            services.AddControllers().AddFluentValidation();
            services.AddMediatR(typeof(CreateAccountRequestHandler).Assembly);
            services.AddSignalR();
            services.AddCors();

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = JwtHelper.GetTokenValidationParameters(
                        JwtHelper.GetPublicKey(_configuration["JwtSettings:PublicKey"]), 
                        _configuration["JwtSettings:Issuer"], 
                        _configuration["JwtSettings:Audience"]);

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        },
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["token"];

                            if (!string.IsNullOrWhiteSpace(accessToken) &&
                                context.Request.Path.StartsWithSegments("/notifications"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireClaim(CustomClaims.Nickname)
                    .RequireClaim(CustomClaims.UserId)
                    .Build();
            });

            services.AddSwaggerGen();

            services.AddHostedService<DelayedMessageDeletionService>();

            services.AddSingleton(Channel.CreateUnbounded<DelayedMessageDeletingTask>());

            services.AddStoredProcedureExecutor(settings =>
            {
                settings.ConnectionString = _configuration["DatabaseSettings:ConnectionString"];
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
        {
            applicationBuilder.UseMiddleware(typeof(ErrorHandlingMiddleware));

            if (env.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chatter API");
            });

            applicationBuilder.UseRouting();

            applicationBuilder.UseCors(policy =>
            {
                policy.SetIsOriginAllowed(x => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            applicationBuilder.UseAuthentication();

            applicationBuilder.UseAuthorization();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationsHub>("/notifications");
                endpoints.MapControllers();
            });
        }
    }
}
