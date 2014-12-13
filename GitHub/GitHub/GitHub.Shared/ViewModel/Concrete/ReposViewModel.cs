using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GitHub.DataObjects.Concrete;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ReposViewModel : SearchViewModelBase, IReposViewModel
    {
        public ReposIncrementalLoadingCollection Repositories { get; private set; }

        public ReposViewModel()
        {
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
            }
        }
        
        public async override Task Refresh()
        {
            Repositories.Reset(SearchValue);
            await Repositories.LoadMoreItemsAsync((uint)Repositories.ItemsPerPage);
        }
    }
}