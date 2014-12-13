using System.Windows.Input;
using GitHub.DataObjects.Concrete;

namespace GitHub.ViewModel.Abstract
{
    public interface IActivitiesViewModel
    {
        ActivitiesIncrementalLoadingCollection Activities { get; }

        ICommand GoToActivityCommand { get; }
    }
}