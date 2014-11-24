/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:GitHub"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using GitHub.Core.Abstract;
using GitHub.Core.Concrete;
using GitHub.ViewModels.ViewModel.Abstract;
using GitHub.ViewModels.ViewModel.Concrete;
using Microsoft.Practices.ServiceLocation;

namespace GitHub.ViewModels.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            // view models
            SimpleIoc.Default.Register<ILoginViewModel, LoginViewModel>();
            SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();

            SimpleIoc.Default.Register<IProfileViewModel, ProfileViewModel>();
            SimpleIoc.Default.Register<IActivitiesViewModel, ActivitiesViewModel>();
            SimpleIoc.Default.Register<IReposViewModel, ReposViewModel>();
            SimpleIoc.Default.Register<IUsersViewModel, UsersViewModel>();

            // services
            SimpleIoc.Default.Register<IGitHubManager, GitHubManager>();
        }

        #endregion Constructor

        #region View Models

        public static ILoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILoginViewModel>();
            }
        }

        public static IMainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IMainViewModel>();
            }
        }

        #endregion View Models

        #region Services

        public static IGitHubManager GitHubManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IGitHubManager>();
            }
        }

        #endregion Services
    }
}