using FocusMark.SDK;
using FocusMark.SDK.Desktop;
using FocusMark.TestUI.ViewModels;
using FocusMark.TestUI.ViewModels.MainWindowViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FocusMark.TestUI
{
    public static class IServiceCollectionExtensions
    {
        public static FocusMarkBuilder AddTestUI(this FocusMarkBuilder focusMarkBuilder, IConfiguration configuration)
        {
            focusMarkBuilder.ServiceRegistery.AddSingleton<IConfiguration>(configuration);
            focusMarkBuilder.AddFocusMarkDesktop();

            AddViewModels(focusMarkBuilder.ServiceRegistery);
            
            return focusMarkBuilder;
        }

        private static void AddViewModels(IServiceCollection services)
        {
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<AccountViewModel>();
        }
    }
}
