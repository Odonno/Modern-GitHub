using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using GalaSoft.MvvmLight;
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
        private readonly ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView("Resources");
        private readonly INavigationService _navigationService;
        private readonly ISessionService _sessionService;
        private readonly ILocalNotificationService _localNotificationService;


        public LoginViewModel(INavigationService navigationService, 
            ILocalNotificationService localNotificationService, 
            ISessionService sessionService)
        {
            _navigationService = navigationService;
            _localNotificationService = localNotificationService;
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

#if WINDOWS_APP
                if (auth == null)
                    isToShowMessage = true;

                if (auth != null && auth.Value)
                    await FinalizeLoginAsync();
#endif
            }
            catch (Exception ex)
            {
                App.TelemetryClient.TrackException(ex);
                isToShowMessage = true;
            }

            if (isToShowMessage)
            {
                _localNotificationService.SendNotification(null, _resourceLoader.GetString("AuthenticationFails"));
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