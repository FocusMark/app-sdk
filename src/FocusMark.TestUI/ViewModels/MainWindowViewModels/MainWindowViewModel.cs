using System.ComponentModel;

namespace FocusMark.TestUI.ViewModels.MainWindowViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(AccountViewModel accountViewModel)
        {
            this.AccountViewModel = accountViewModel ?? throw new System.ArgumentNullException(nameof(accountViewModel));
        }

        public AccountViewModel AccountViewModel { get; set; }
    }
}
