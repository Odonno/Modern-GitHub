using System;
using System.Linq;
using Windows.Security.Credentials;
using GitHub.Core.Abstract;

namespace GitHub.Core.Concrete
{
    public class CredentialManager : ICredentialManager
    {
        private const string _credentialResourceName = "UniversalGitHubCredential";

        public void SaveCredential(string userName, string password)
        {
            var vault = new PasswordVault();
            var credential = new PasswordCredential(_credentialResourceName, userName, password);

            // permanently stores credential in the password vault.
            vault.Add(credential);
        }

        public PasswordCredential GetCredential()
        {
            var vault = new PasswordVault();

            try
            {
                var credential = vault.FindAllByResource(_credentialResourceName).FirstOrDefault();
                if (credential != null)
                {
                    // retrieves the actual userName and password.
                    string userName = credential.UserName;
                    return vault.Retrieve(_credentialResourceName, userName);
                }
            }
            catch (Exception)
            {
                // if no credentials have been stored with the given RESOURCE_NAME, an exception is thrown.
            }

            return null;
        }

        public void RemoveCredential(string userName)
        {
            var vault = new PasswordVault();
            try
            {
                // removes the credential from the password vault.
                vault.Remove(vault.Retrieve(_credentialResourceName, userName));
            }
            catch (Exception)
            {
                // if no credentials have been stored with the given RESOURCE_NAME, an exception is thrown.
            }
        }
    }
}
