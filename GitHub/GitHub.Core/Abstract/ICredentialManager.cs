using Windows.Security.Credentials;

namespace GitHub.Core.Abstract
{
    public interface ICredentialManager
    {
        void SaveCredential(string userName, string password);
        PasswordCredential GetCredential();
        void RemoveCredential(string userName);
    }
}
