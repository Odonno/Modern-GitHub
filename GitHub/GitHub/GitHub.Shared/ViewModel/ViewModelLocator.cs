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
using GalaSoft.MvvmLight.Views;
using GitHub.DataObjects.Abstract;
using GitHub.DataObjects.Concrete;
using GitHub.Services.Abstract;
using GitHub.Services.Concrete;
using GitHub.ViewModel.Abstract;
using GitHub.ViewModel.Concrete;
using GitHub.Views;
using Microsoft.Practices.ServiceLocation;
using Octokit;

namespace GitHub.ViewModel
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
            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                var navigationService = CreateNavigationService();
                SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            }

            if (!SimpleIoc.Default.IsRegistered<IDialogService>())
            {
                var dialogService = new DialogService();
                SimpleIoc.Default.Register<IDialogService>(() => dialogService);
            }

            SimpleIoc.Default.Register<ISessionService, SessionService>();
            SimpleIoc.Default.Register<IGitHubService, GitHubService>();

            // data objects
            SimpleIoc.Default.Register<UsersIncrementalLoadingCollection>();

            // model
            if (!SimpleIoc.Default.IsRegistered<IGitHubClient>())
            {
                var client = new GitHubClient(new ProductHeaderValue("UniversalGitHub"));
                SimpleIoc.Default.Register<IGitHubClient>(() => client);
            }
        }

        #endregion Constructor

        #region Methods

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("SplashScreen", typeof(SplashScreenPage));
            navigationService.Configure("Main", typeof(MainPage));

            return navigationService;
        }

        #endregion



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

        public static IProfileViewModel Profile
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IProfileViewModel>();
            }
        }

        public static IActivitiesViewModel Activities
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IActivitiesViewModel>();
            }
        }

        public static IReposViewModel Repos
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IReposViewModel>();
            }
        }

        public static IUsersViewModel Users
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUsersViewModel>();
            }
        }

        #endregion View Models

        #region Services

        public static IGitHubService GitHubService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IGitHubService>();
            }
        }

        #endregion Services

        #region Model

        public static IGitHubClient GitHubClient
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IGitHubClient>();
            }
        }

        #endregion
    }
}