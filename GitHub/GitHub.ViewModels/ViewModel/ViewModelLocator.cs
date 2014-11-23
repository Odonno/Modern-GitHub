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
using GitHub.Core;
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
            SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();

            // services
            SimpleIoc.Default.Register<IGitHubManager, GitHubManager>();
        }

        #endregion


        #region View Models

        public static IMainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IMainViewModel>();
            }
        }

        #endregion


        #region Services

        public static IGitHubManager GitHubManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IGitHubManager>();
            }
        }

        #endregion
    }
}