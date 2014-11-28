using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ReposViewModel : ViewModelBase, IReposViewModel
    {
        public ObservableCollection<Repository> Repositories { get; private set; }
        public ICommand SearchCommand { get; private set; }


        public ReposViewModel()
        {
            //if (IsInDesignMode)
            //{
            //    // Code runs in Blend --> create design time data.
            //}
            //else
            //{
            //    // Code runs "for real"
            //}
        }
    }
}