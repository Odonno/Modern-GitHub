using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHub.Services.Abstract
{
    public interface IBackgroundTaskService
    {
        Dictionary<string, string> Tasks { get; }

        Task RegisterTasksAsync();
    }
}
