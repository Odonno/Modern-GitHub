using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using GitHub.Services.Abstract;
using GitHub.ViewModel;
using Octokit;

#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Activation;
#endif

namespace GitHub.Services.Concrete
{
    public class SessionService : ISessionService
    {
        private const string _clientId = "cf9380704e8a73863446";
        private const string _clientSecret = "65235fc4bef14b408f43ac1a971e1c16c0a310cd";

        private OauthToken _oauthToken;

        private readonly IGitHubClient _client;

        public SessionService()
        {
            _client = ViewModelLocator.GitHubClient;
        }


        public async Task<bool?> LoginAsync()
        {
            try
            {
                var startUri = _client.Oauth.GetGitHubLoginUrl(new OauthLoginRequest(_clientId));
                var endUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

#if WINDOWS_PHONE_APP
                WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);
                return null;
#else
                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
                return await GetSession(webAuthenticationResult);
#endif
            }
            catch
            {
                return null;
            }
        }

        public void Logout()
        {
        }


#if WINDOWS_PHONE_APP
        public async Task<bool> Finalize(WebAuthenticationBrokerContinuationEventArgs args)
        {
            try
            {
                return await GetSession(args.WebAuthenticationResult);
            }
            catch
            {
            }

            return false;
        }
#endif


        private async Task<bool> GetSession(WebAuthenticationResult result)
        {
            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var code = GetCode(result.ResponseData);
                _oauthToken = await GetToken(code);

                _client.Connection.Credentials = new Credentials(_oauthToken.AccessToken);

                return true;
            }
            if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                throw new Exception("Error http");
            }
            if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
            {
                throw new Exception("User Canceled.");
            }

            return false;
        }

        private string GetCode(string webAuthResultResponseData)
        {
            var splitResultResponse = webAuthResultResponseData.Split('&');
            var codeString = splitResultResponse.FirstOrDefault(value => value.Contains("code"));
            var splitCode = codeString.Split('=');
            return splitCode.Last();
        }

        private async Task<OauthToken> GetToken(string code)
        {
            return await _client.Oauth.CreateAccessToken(new OauthTokenRequest(_clientId, _clientSecret, code));
        }
    }
}
