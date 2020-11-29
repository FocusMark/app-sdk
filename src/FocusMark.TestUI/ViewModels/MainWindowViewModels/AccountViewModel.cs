using FocusMark.SDK;
using FocusMark.SDK.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace FocusMark.TestUI.ViewModels.MainWindowViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly ILoginService loginService;
        private readonly IAccountService accountService;

        public AccountViewModel(ILoginService loginService, IAccountService accountService)
        {
            this.loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserId { get; private set; } = "None";

        public string Scopes { get; private set; } = "None";

        public async void AuthorizeUser()
        {
            ServiceResponse<LoginResponse> authResponse = await this.accountService.AuthorizeUser();

            if (!authResponse.IsSuccessful)
            {
                // TODO: Error handle
                return;
            }

            AccessToken accessToken = authResponse.Data.AccessToken;
            this.UserId = accessToken.UserId;
            this.Scopes = string.Join(", ", accessToken.Scopes);
            this.OnPropertyChanged(nameof(UserId));
            this.OnPropertyChanged(nameof(Scopes));
        }

        private void OnPropertyChanged(string property)
        {
            var changedEvent = new PropertyChangedEventArgs(property);
            this.PropertyChanged?.Invoke(this, changedEvent);
        }
    }
}
