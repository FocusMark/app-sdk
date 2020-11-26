using FocusMark.SDK;
using FocusMark.SDK.Account;
using FocusMark.SDK.Desktop;
using FocusMark.TestUI.ViewModels;
using FocusMark.TestUI.ViewModels.MainWindowViewModels;
using MahApps.Metro.Controls;
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
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public IServiceProvider Services { get; set; }

        public AccountViewModel AccountViewModel => ((MainWindowViewModel)this.DataContext).AccountViewModel;

        protected override void OnInitialized(EventArgs e)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("79e07d49-7921-4237-8c0f-03c3339ffbca")
                .Build();

            this.Services = new ServiceCollection()
                .AddFocusMark(builder => builder.AddTestUI(configuration))
                .BuildServiceProvider();

            this.DataContext = this.Services.GetRequiredService<MainWindowViewModel>();
            base.OnInitialized(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.AccountViewModel.AuthorizeUser();
        }
    }
}
