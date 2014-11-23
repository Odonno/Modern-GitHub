using System.Windows.Input;
using Octokit;

namespace GitHub.ViewModels.ViewModel.Abstract
{
    public interface IProfileViewModel
    {
        User CurrentUser { get; }

        ICommand LoadCommand { get; }
    }
}