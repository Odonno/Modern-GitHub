using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GitHub.DataObjects.Concrete;
using GitHub.Services.Abstract;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : SearchViewModelBase, IUsersViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IProgressIndicatorService _progressIndicatorService;


        private readonly UsersIncrementalLoadingCollection _users;
        public UsersIncrementalLoadingCollection Users { get { return _users; } }

        public ICommand GoToUserCommand { get; private set; }


        public UsersViewModel(INavigationService navigationService,
            IProgressIndicatorService progressIndicatorService)
        {
            _navigationService = navigationService;
            _progressIndicatorService = progressIndicatorService;

            _users = SimpleIoc.Default.GetInstance<UsersIncrementalLoadingCollection>();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                var odonno = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);

                var corentinMiq = new User("https://github.com/identicons/CorentinMiq.png",
                    null, null, 200, null, new DateTimeOffset(), 0, null, 7, 84, null, null, 0,
                    2, null,
                    "CorentinMiq", "Corentin Miquée",
                    0, null, 0, 0, 3, null, false);

                Users.Add(odonno);
                Users.Add(corentinMiq);
            }
            else
            {
                // Code runs "for real"

                // TODO : first request on last registered users ?

                GoToUserCommand = new RelayCommand<User>(GoToUser);
            }
        }

        public async override Task Refresh()
        {
            Users.Reset(SearchValue);
            await Users.LoadMoreItemsAsync((uint)Users.ItemsPerPage);
        }

        private async void GoToUser(User user)
        {
            await _progressIndicatorService.ShowAsync();

            ViewModelLocator.User.User = user;
            _navigationService.NavigateTo("User");

            await ViewModelLocator.User.LoadUserDataAsync();

            await _progressIndicatorService.HideAsync();
        }
    }
}