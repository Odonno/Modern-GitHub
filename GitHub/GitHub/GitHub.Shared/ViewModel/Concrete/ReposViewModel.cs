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
    public class ReposViewModel : SearchViewModelBase, IReposViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IProgressIndicatorService _progressIndicatorService;

        public ReposIncrementalLoadingCollection Repositories { get; private set; }

        public ICommand GoToRepoCommand { get; private set; }


        public ReposViewModel(INavigationService navigationService,
            IProgressIndicatorService progressIndicatorService)
        {
            _navigationService = navigationService;
            _progressIndicatorService = progressIndicatorService;

            Repositories = SimpleIoc.Default.GetInstance<ReposIncrementalLoadingCollection>();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                var firstRepo = new Repository(null, null, null, null, null, null, null,
                    1,
                    null,
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

                var anotherRepo = new Repository(null, null, null, null, null, null, null,
                    2,
                    null,
                    "Another-Repository",
                    "Odonno/Another-Repository",
                    null, null, null,
                    false, false,
                    0, 0, 0,
                    "master",
                    0,
                    null, new DateTimeOffset(), new DateTimeOffset(),
                    null, null, null, null,
                    false, false, false);

                Repositories.Add(firstRepo);
                Repositories.Add(anotherRepo);
            }
            else
            {
                // Code runs "for real"

                // TODO : first request on last created repos ?

                GoToRepoCommand = new RelayCommand<Repository>(GoToRepostiory);
            }
        }
        
        public async override Task Refresh()
        {
            Repositories.Reset(SearchValue);
            await Repositories.LoadMoreItemsAsync((uint)Repositories.ItemsPerPage);
        }

        private async void GoToRepostiory(Repository repository)
        {
            await _progressIndicatorService.ShowAsync();

            ViewModelLocator.Repository.Repository = repository;
            _navigationService.NavigateTo("Repository");

            await ViewModelLocator.Repository.LoadRepositoryDataAsync();

            await _progressIndicatorService.HideAsync();
        }
    }
}