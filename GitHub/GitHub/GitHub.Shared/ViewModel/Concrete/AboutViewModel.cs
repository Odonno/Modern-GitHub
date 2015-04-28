using Windows.ApplicationModel;
using GalaSoft.MvvmLight;
using GitHub.ViewModel.Abstract;

namespace GitHub.ViewModel.Concrete
{
    public class AboutViewModel : ViewModelBase, IAboutViewModel
    {
        public string AppVersion { get; private set; }


        public AboutViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                AppVersion = "0.4.2.0";
            }
            else
            {
                // Code runs "for real"
                var version = Package.Current.Id.Version;

                AppVersion = string.Format("{0}.{1}.{2}.{3}",
                    version.Major,
                    version.Minor,
                    version.Build,
                    version.Revision);
            }
        }
    }
}
