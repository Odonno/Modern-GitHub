using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : ViewModelBase, IUsersViewModel
    {
        private readonly ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users { get { return _users; } }

        private string _searchName;
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                RaisePropertyChanged();
                ((RelayCommand)SearchCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SearchCommand { get; private set; }


        public UsersViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                _users.Add(new User
                {
                    Login = "Odonno",
                    Followers = 144,
                    Following = 3,
                    PublicRepos = 44,
                    AvatarUrl = "https://github.com/identicons/odonno.png"
                });
                _users.Add(new User
                {
                    Login = "CorentinMiq",
                    Followers = 7,
                    Following = 84,
                    PublicRepos = 3,
                    AvatarUrl = "https://github.com/identicons/CorentinMiq.png"
                });
            }
            else
            {
                // Code runs "for real"
            }

            SearchCommand = new RelayCommand(Load, CanLoad);
        }


        private bool CanLoad()
        {
            return !string.IsNullOrWhiteSpace(SearchName);
        }
        private async void Load()
        {
            // TODO : instead request for last registered users
            var result = await ViewModelLocator.GitHubService.SearchUsersAsync(SearchName);

            Users.Clear();
            foreach (var item in result.Items)
                Users.Add(item);
        }
    }
}