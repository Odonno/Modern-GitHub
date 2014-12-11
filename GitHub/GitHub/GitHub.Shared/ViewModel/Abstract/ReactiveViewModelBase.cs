using Windows.ApplicationModel;
using ReactiveUI;

namespace GitHub.ViewModel.Abstract
{
    public abstract class ReactiveViewModelBase : ReactiveObject
    {
        private static bool? _isInDesignMode;

        public bool IsInDesignMode
        {
            get
            {
                return IsInDesignModeStatic;
            }
        }

        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                    _isInDesignMode = DesignMode.DesignModeEnabled;

                return _isInDesignMode.Value;
            }
        }
    }
}