using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class RepositoryViewModel : ViewModelBase, IRepositoryViewModel
    {
        private Repository _repository;
        public Repository Repository
        {
            get { return _repository; }
            set
            {
                _repository = value;
                RaisePropertyChanged();
            }
        }


        public RepositoryViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data

                Repository = new Repository(null, null, null, null, null, null, null,
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
            }
            else
            {
                // Code runs "for real"
            }
        }


        public async Task LoadRepoDataAsync()
        {
            // TODO : load repository data
        }
    }
}
