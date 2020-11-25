using FocusMark.SDK;
using FocusMark.SDK.Account;
using FocusMark.SDK.Desktop;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Windows;

namespace FocusMark.TestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("79e07d49-7921-4237-8c0f-03c3339ffbca")
                .Build();

            this.Services = new ServiceCollection()
                .AddFocusMark(builder => builder.AddFocusMarkDesktop())
                .AddHttpClient()
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();
        }

        public IServiceProvider Services { get; set; }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ILoginService loginService = this.Services.GetRequiredService<ILoginService>();
            ServiceResponse loginResponse = await loginService.Login();

            if (loginResponse.IsSuccessful)
            {
                return;
            }

            // handle errors here
        }
    }
}
