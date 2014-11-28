using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class ActivitiesViewModel : ViewModelBase, IActivitiesViewModel
    {
        public ObservableCollection<Activity> Activities { get; private set; }
        public ICommand SearchCommand { get; private set; }


        public ActivitiesViewModel()
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