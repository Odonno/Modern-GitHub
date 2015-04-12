using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitHub.Services.Abstract;
using GitHub.ViewModel.Abstract;

#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Activation;
#endif

namespace GitHub.ViewModel.Concrete
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ISessionService _sessionService;
        private readonly IDialogService _dialogService;


        public LoginViewModel(INavigationService navigationService, IDialogService dialogService, ISessionService sessionService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _sessionService = sessionService;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }


        public async Task LoginAsync()
        {
            bool isToShowMessage = false;

            try
            {
                var auth = await _sessionService.LoginAsync();

                if (auth == null)
                    isToShowMessage = true;

                if (auth != null && auth.Value)
                    await FinalizeLoginAsync();
            }
            catch
            {
                isToShowMessage = true;
            }

            if (isToShowMessage)
            {
                await _dialogService.ShowError("Application fails",
                    "Authentication",
                    "Ok",
                    null);
            }
        }

        private async Task FinalizeLoginAsync()
        {
            await ViewModelLocator.Profile.LoadAsync();
            _navigationService.NavigateTo("Main");
        }


#if WINDOWS_PHONE_APP
        public async void Finalize(WebAuthenticationBrokerContinuationEventArgs args)
        {
            var result = await _sessionService.Finalize(args);
            if (result)
                await FinalizeLoginAsync();
        }
#endif
    }
}