using System.Windows.Input;
#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Activation;
#endif

namespace GitHub.ViewModel.Abstract
{
    public interface ILoginViewModel
    {
        ICommand LoginCommand { get; }
        void Login();

#if WINDOWS_PHONE_APP
        void Finalize(WebAuthenticationBrokerContinuationEventArgs args);
#endif
    }
}