using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string email;
        public string Email
        {
            get { return email; }
            set 
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set 
            { 
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginCommand { get; }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage { set => ErrorMessageViewModel.Message = value; }

        public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator, IRenavigator loginRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            ViewLoginCommand = new RenavigateCommand(loginRenavigator);
            RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
        }
    }
}
