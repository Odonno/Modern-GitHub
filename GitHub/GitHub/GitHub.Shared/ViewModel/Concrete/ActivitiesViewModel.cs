using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GitHub.DataObjects.Concrete;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ActivitiesViewModel : SearchViewModelBase, IActivitiesViewModel
    {
        public ActivitiesIncrementalLoadingCollection Activities { get; private set; }

        public ActivitiesViewModel()
        {
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
                    CreatedAt = new DateTimeOffset(new DateTime(2014, 12, 11)),
                    Public = true,
                    Repo = firstRepo,
                    Type = "CreateEvent"
                });
                Activities.Add(new Activity
                {
                    Actor = odonno,
                    CreatedAt = new DateTimeOffset(new DateTime(2014, 12, 12)),
                    Public = true,
                    Repo = firstRepo,
                    Type = "PushEvent"
                });
            }
            else
            {
                // Code runs "for real"

                // TODO : first request on last activities of current user ?
                Activities.LoadMoreItemsAsync(30);
            }
        }

        protected async override Task CompleteSearch()
        {
            Activities.Reset(SearchValue);
            await Activities.LoadMoreItemsAsync((uint)Activities.ItemsPerPage);
        }
    }
}