using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GitHub.DataObjects.Concrete;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ActivitiesViewModel : SearchViewModelBase, IActivitiesViewModel
    {
        private readonly INavigationService _navigationService;

        public ActivitiesIncrementalLoadingCollection Activities { get; private set; }

        public ICommand GoToActivityCommand { get; private set; }


        public ActivitiesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Activities = SimpleIoc.Default.GetInstance<ActivitiesIncrementalLoadingCollection>();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                var odonno = new User
                {
                    Login = "Odonno",
                    Followers = 144,
                    Following = 3,
                    PublicRepos = 44,
                    AvatarUrl = "https://github.com/identicons/odonno.png"
                };
                var firstRepo = new Repository { Name = "First Repository" };

                Activities.Add(new Activity
                {
                    Actor = odonno,
                    CreatedAt = new DateTimeOffset(new DateTime(2014, 12, 12)),
                    Public = true,
                    Repo = firstRepo,
                    Type = "PushEvent"
                });
                Activities.Add(new Activity
                {
                    Actor = odonno,
                    CreatedAt = new DateTimeOffset(new DateTime(2014, 12, 11)),
                    Public = true,
                    Repo = firstRepo,
                    Type = "CreateEvent"
                });
            }
            else
            {
                // Code runs "for real"

                // TODO : first request on last activities of current user ?
                Refresh();

                GoToActivityCommand = new RelayCommand<Activity>(GoToActivity);
            }
        }
        
        public async override Task Refresh()
        {
            Activities.Reset(SearchValue);
            await Activities.LoadMoreItemsAsync((uint)Activities.ItemsPerPage);
        }

        private void GoToActivity(Activity activity)
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }
    }
}