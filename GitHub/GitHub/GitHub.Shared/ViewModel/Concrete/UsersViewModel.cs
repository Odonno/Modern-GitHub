using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : ViewModelBase, IUsersViewModel
    {
        private readonly ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users { get { return _users; } }

        public ICommand SearchCommand { get; private set; }


        public UsersViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                _users.Add(new User { Login = "Odonno" });
                _users.Add(new User { Login = "CorMiq" });
            }
            else
            {
                // Code runs "for real"

                Load();
            }
        }

        private async Task Load()
        {
            // TODO : instead request for last registered users
            var result = await ViewModelLocator.GitHubService.SearchUsersAsync("o");

            Users.Clear();
            foreach (var item in result.Items)
                Users.Add(item);
        }
    }
}