﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GitHub.DataObjects.Concrete;
using GitHub.ViewModel.Abstract;
using Octokit;

namespace GitHub.ViewModel.Concrete
{
    public class UsersViewModel : SearchViewModelBase, IUsersViewModel
    {
        private readonly INavigationService _navigationService;

        private readonly UsersIncrementalLoadingCollection _users;
        public UsersIncrementalLoadingCollection Users { get { return _users; } }

        public ICommand GoToUserCommand { get; private set; }


        public UsersViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _users = SimpleIoc.Default.GetInstance<UsersIncrementalLoadingCollection>();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                //Users.Add(new User
                //{
                //    Login = "Odonno",
                //    Followers = 144,
                //    Following = 3,
                //    PublicRepos = 44,
                //    AvatarUrl = "https://github.com/identicons/odonno.png"
                //});
                //Users.Add(new User
                //{
                //    Login = "CorentinMiq",
                //    Followers = 7,
                //    Following = 84,
                //    PublicRepos = 3,
                //    AvatarUrl = "https://github.com/identicons/CorentinMiq.png"
                //});
            }
            else
            {
                // Code runs "for real"

                // TODO : first request on last registered users ?

                GoToUserCommand = new RelayCommand<User>(GoToUser);
            }
        }

        public async override Task Refresh()
        {
            Users.Reset(SearchValue);
            await Users.LoadMoreItemsAsync((uint)Users.ItemsPerPage);
        }

        private async void GoToUser(User user)
        {
            ViewModelLocator.User.User = user;
            _navigationService.NavigateTo("User");

            await ViewModelLocator.User.LoadUserDataAsync();
        }
    }
}