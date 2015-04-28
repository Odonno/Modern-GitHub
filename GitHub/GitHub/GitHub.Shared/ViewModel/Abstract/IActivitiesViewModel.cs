using System.Collections.ObjectModel;
using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IActivitiesViewModel
    {
        ObservableCollection<Activity> Activities { get; }

        ICommand GoToActivityCommand { get; }
    }
}