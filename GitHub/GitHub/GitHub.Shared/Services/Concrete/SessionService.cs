using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using GitHub.Services.Abstract;
using Octokit;

#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Activation;
#endif

namespace GitHub.Services.Concrete
{
    public class SessionService : ISessionService
    {
#if WINDOWS_PHONE_APP
        private const string ClientId = "cf9380704e8a73863446";
        private const string ClientSecret = "65235fc4bef14b408f43ac1a971e1c16c0a310cd";
#else
        private const string ClientId = "fea5e25a11932b2d3f96";
        private const string ClientSecret = "1e60e28b73bd561715be6f8d149483537d627d3e";
#endif

        private static readonly Collection<string> Scopes = new Collection<string>(new[] { "user:follow", "notifications" });


        private readonly IGitHubClient _client;
        private readonly IGitHubService _gitHubService;


        public SessionService(IGitHubClient client, IGitHubService gitHubService)
        {
            _client = client;
            _gitHubService = gitHubService;
        }


        public async Task<bool?> LoginAsync()
        {
            try
            {
                // create OAuth request (with scopes)
                var oauthLoginRequest = new OauthLoginRequest(ClientId);
                foreach (var scope in Scopes)
                    oauthLoginRequest.Scopes.Add(scope);

                var startUri = _client.Oauth.GetGitHubLoginUrl(oauthLoginRequest);
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
                var token = await GetToken(code);

                _gitHubService.TryAuthenticate(token);

                return true;
            }
            if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                throw new Exception("Error http");
            }
            if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
            {
                throw new Exception("User Canceled");
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
            return await _client.Oauth.CreateAccessToken(new OauthTokenRequest(ClientId, ClientSecret, code));
        }
    }
}
