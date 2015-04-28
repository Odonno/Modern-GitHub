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

            // ViewModels

            // First ViewModel (Global)
            SimpleIoc.Default.Register<ILoginViewModel, LoginViewModel>();
            SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();

            // Top level ViewModels
            SimpleIoc.Default.Register<IProfileViewModel, ProfileViewModel>();
            SimpleIoc.Default.Register<IActivitiesViewModel, ActivitiesViewModel>();
            SimpleIoc.Default.Register<IReposViewModel, ReposViewModel>();
            SimpleIoc.Default.Register<IUsersViewModel, UsersViewModel>();

            // Second Level ViewModels
            SimpleIoc.Default.Register<IUserViewModel, UserViewModel>();
            SimpleIoc.Default.Register<IRepositoryViewModel, RepositoryViewModel>();

            // Other Level ViewModels
            SimpleIoc.Default.Register<IAboutViewModel, AboutViewModel>();
            SimpleIoc.Default.Register<ICreditsViewModel, CreditsViewModel>();
            SimpleIoc.Default.Register<IFeedbackViewModel, FeedbackViewModel>();
            SimpleIoc.Default.Register<ISettingsViewModel, SettingsViewModel>();
            

            // Services
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

            if (!SimpleIoc.Default.IsRegistered<IProgressIndicatorService>())
            {
                var progressIndicatorService = new ProgressIndicatorService();
                SimpleIoc.Default.Register<IProgressIndicatorService>(() => progressIndicatorService);
            }

            SimpleIoc.Default.Register<ISessionService, SessionService>();
            SimpleIoc.Default.Register<IGitHubService, GitHubService>();

            SimpleIoc.Default.Register<IBackgroundTaskService, BackgroundTaskService>();
            SimpleIoc.Default.Register<ILocalNotificationService, LocalNotificationService>();

            // data objects
            SimpleIoc.Default.Register<UsersIncrementalLoadingCollection>();
            SimpleIoc.Default.Register<ReposIncrementalLoadingCollection>();

            // model
            if (!SimpleIoc.Default.IsRegistered<IGitHubClient>())
            {
#if WINDOWS_PHONE_APP
                var client = new GitHubClient(new ProductHeaderValue("UniversalGitHub"));
#else
                var client = new GitHubClient(new ProductHeaderValue("UniversalGitHubW8"));
#endif
                SimpleIoc.Default.Register<IGitHubClient>(() => client);
            }
        }

        #endregion Constructor


        #region Navigation Service (Page declaration)

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure("SplashScreen", typeof(SplashScreenPage));
            navigationService.Configure("Main", typeof(MainPage));
            navigationService.Configure("InDevelopment", typeof(InDevelopmentPage));
            navigationService.Configure("User", typeof(UserPage));
            navigationService.Configure("Repository", typeof(RepositoryPage));

            navigationService.Configure("MyCollaborators", typeof(MyCollaboratorsPage));
            navigationService.Configure("MyFollowers", typeof(MyFollowersPage));
            navigationService.Configure("MyFollowings", typeof(MyFollowingsPage));
            navigationService.Configure("MyPrivateRepos", typeof(MyPrivateReposPage));
            navigationService.Configure("MyGists", typeof(MyGistsPage));
            navigationService.Configure("MyPublicRepos", typeof(MyPublicReposPage));

            navigationService.Configure("About", typeof(AboutPage));
            navigationService.Configure("Credits", typeof(CreditsPage));
            navigationService.Configure("Feedback", typeof(FeedbackPage));
            navigationService.Configure("Settings", typeof(SettingsPage));

            return navigationService;
        }

        #endregion


        #region Services

        public static IGitHubService GitHubService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IGitHubService>();
            }
        }

        #endregion


        #region View Models

        public static IAboutViewModel About
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IAboutViewModel>();
            }
        }

        public static ICreditsViewModel Credits
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICreditsViewModel>();
            }
        }

        public static IFeedbackViewModel Feedback
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IFeedbackViewModel>();
            }
        }

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

        public static ISettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISettingsViewModel>();
            }
        }

        public static IReposViewModel Repos
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IReposViewModel>();
            }
        }

        public static IRepositoryViewModel Repository
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IRepositoryViewModel>();
            }
        }

        public static IUsersViewModel Users
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUsersViewModel>();
            }
        }

        public static IUserViewModel User
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUserViewModel>();
            }
        }

        #endregion View Models
    }
}