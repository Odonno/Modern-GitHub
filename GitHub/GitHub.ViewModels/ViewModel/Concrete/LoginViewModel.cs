using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitHub.ViewModels.ViewModel.Abstract;

namespace GitHub.ViewModels.ViewModel.Concrete
{
    internal class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; private set; }


        public LoginViewModel()
        {
            //if (IsInDesignMode)
            //{
            //    // Code runs in Blend --> create design time data.
            //}
            //else
            //{
            //    // Code runs "for real"
            //}

            LoginCommand = new RelayCommand(Login, CanLogin);
        }


        private bool CanLogin()
        {
            throw new NotImplementedException();
        }
        private void Login()
        {
            throw new NotImplementedException();
        }
    }
}