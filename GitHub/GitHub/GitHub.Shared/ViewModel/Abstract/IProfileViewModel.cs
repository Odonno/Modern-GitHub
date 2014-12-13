using System.Threading.Tasks;
using Octokit;

namespace GitHub.ViewModel.Abstract
{
    public interface IProfileViewModel
    {
        User CurrentUser { get; }

        Task Load();
    }
}