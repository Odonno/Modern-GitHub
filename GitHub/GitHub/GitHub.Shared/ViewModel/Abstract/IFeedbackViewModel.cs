using System.Collections.Generic;
using System.Windows.Input;

namespace GitHub.ViewModel.Abstract
{
    public interface IFeedbackViewModel
    {
        IEnumerable<string> FeedbackTypes { get; }
        string SelectedFeedbackType { get; set; }
        string Content { get; set; }

        ICommand SendFeedbackCommand { get; }
    }
}
