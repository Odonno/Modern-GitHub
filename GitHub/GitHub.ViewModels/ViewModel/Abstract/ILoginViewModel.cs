using System.Windows.Input;

namespace GitHub.ViewModels.ViewModel.Abstract
{
    public interface ILoginViewModel
    {
        string Username { get; set; }

        string Password { get; set; }

        ICommand LoginCommand { get; }
    }
}