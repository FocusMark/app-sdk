using FocusMark.SDK;
using FocusMark.SDK.Account;
using Microsoft.Extensions.DependencyInjection;

namespace FocusMark.SDK.Desktop
{
    public static class ServiceCollectionExtensions
    {
        public static FocusMarkBuilder AddFocusMarkDesktop(this FocusMarkBuilder builder)
        {
            builder.ServiceRegistery.AddSingleton<ILoginService, DesktopLoginService>();
            return builder;
        }
    }
}
