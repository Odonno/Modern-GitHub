using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ActivitiesViewModel : SearchViewModelBase, IActivitiesViewModel
    {
        private readonly INavigationService _navigationService;
        

        private readonly ObservableCollection<Activity> _activities = new ObservableCollection<Activity>();
        public ObservableCollection<Activity> Activities { get { return _activities; } }

        public ICommand GoToActivityCommand { get; private set; }


        public ActivitiesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                var odonno = new User("https://github.com/identicons/odonno.png",
                    null, null, 100, null, new DateTimeOffset(), 0, null, 144, 3, null, null, 0,
                    1, null,
                    "Odonno", "David Bottiau",
                    0, null, 0, 0, 44, null, false);

                var firstRepo = new Repository(null, null, null, null, null, null, null,
                    1,
                    odonno,
                    "First-Repository",
                    "Odonno/First-Repository",
                    null, null, null,
                    false, false,
                    0, 0, 0,
                    "master",
                    0,
                    null, new DateTimeOffset(), new DateTimeOffset(),
                    null, null, null, null,
                    false, false, false);

                var pushActivity = new Activity("PushEvent", true, firstRepo, odonno, null,
                    new DateTimeOffset(new DateTime(2014, 12, 12)), "1");

                var createActivity = new Activity("CreateEvent", true, firstRepo, odonno, null,
                    new DateTimeOffset(new DateTime(2014, 12, 11)), "2");

                Activities.Add(pushActivity);
                Activities.Add(createActivity);
            }
            else
            {
                // Code runs "for real"

                // first request on last activities of current user ?
                Refresh();

                GoToActivityCommand = new RelayCommand<Activity>(GoToActivity);
            }
        }

        public async override Task Refresh()
        {
            var activities = await ViewModelLocator.GitHubService.GetUserActivitiesAsync(ViewModelLocator.Profile.CurrentUser.Login);

            Activities.Clear();
            foreach (var activity in activities)
            {
                Activities.Add(activity);
            }
        }

        private void GoToActivity(Activity activity)
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }
    }
}