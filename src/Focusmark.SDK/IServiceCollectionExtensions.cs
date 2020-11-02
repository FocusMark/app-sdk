using FocusMark.SDK.Account;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FocusMark.SDK
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFocusMark(this IServiceCollection services, Action<FocusMarkBuilder> focusmarkBuilder)
        {
            services.AddSingleton<ITokenRepository, TokenLiteDbRepository>();
            services.AddSingleton<IAccountService, OAuthAuthorizationService>();
            services.AddSingleton<IDatabaseFactory, LiteDatabaseFactory>();

            focusmarkBuilder.Invoke(new FocusMarkBuilder(services));

            // If the builder was not used to add a platform specific implementation of ILoginService then we throw.
            // An implementation of ILoginService is required for the SDK to work. We will not accept tokens that was not created by the SDK.
            if (services.Any(service => service.ServiceType == typeof(ILoginService)))
            {
                throw new InvalidOperationException($"You can not add FocusMark without also configuring platform specific services via a platform specific extension method on {typeof(FocusMarkBuilder).Name}");
            }

            return services;
        }
    }
}
