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
    public class ReposViewModel : SearchViewModelBase, IReposViewModel
    {
        private readonly INavigationService _navigationService;

        public ReposIncrementalLoadingCollection Repositories { get; private set; }

        public ICommand GoToRepoCommand { get; private set; }


        public ReposViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Repositories = SimpleIoc.Default.GetInstance<ReposIncrementalLoadingCollection>();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                Repositories.Add(new Repository { Name = "First-Repository", FullName = "Odonno/First-Repository" });
                Repositories.Add(new Repository { Name = "Another-Repository", FullName = "Odonno/Another-Repository" });
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

        private void GoToRepostiory(Repository repository)
        {
            // TODO : implement new page
            _navigationService.NavigateTo("InDevelopment");
        }
    }
}