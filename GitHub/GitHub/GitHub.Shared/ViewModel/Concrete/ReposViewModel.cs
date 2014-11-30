using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ReposViewModel : ViewModelBase, IReposViewModel
    {
        private readonly ObservableCollection<Repository> _repositories = new ObservableCollection<Repository>();
        public ObservableCollection<Repository> Repositories { get { return _repositories; } }

        public ICommand SearchCommand { get; private set; }


        public ReposViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                _repositories.Add(new Repository { Name = "First Repository" });
                _repositories.Add(new Repository { Name = "Another Repository" });
            }
            else
            {
                // Code runs "for real"

                Load();
            }
        }


        private async Task Load()
        {
            // TODO : instead request for last created repostiories
            var result = await ViewModelLocator.GitHubService.SearchReposAsync("mvvm");

            Repositories.Clear();
            foreach (var item in result.Items)
                Repositories.Add(item);
        }
    }
}