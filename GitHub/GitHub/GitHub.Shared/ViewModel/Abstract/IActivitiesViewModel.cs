using GitHub.DataObjects.Concrete;

namespace GitHub.ViewModel.Abstract
{
    public interface IActivitiesViewModel
    {
        ActivitiesIncrementalLoadingCollection Activities { get; }
    }
}