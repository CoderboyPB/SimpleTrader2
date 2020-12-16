using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SimpleTrader.Domain.Services.AuthenticationServices.IAuthenticationService;

namespace SimpleTrader.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel registerViewModel;
        private readonly IAuthenticator authenticator;
        private readonly IRenavigator registerRenavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator registerRenavigator)
        {
            this.registerViewModel = registerViewModel;
            this.authenticator = authenticator;
            this.registerRenavigator = registerRenavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            registerViewModel.ErrorMessage = string.Empty;

            //try
            //{
                RegistrationResult registrationResult = await authenticator.Register(
                        registerViewModel.Email,
                        registerViewModel.Username,
                        registerViewModel.Password,
                        registerViewModel.ConfirmPassword);

                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        registerRenavigator.Renavigate();
                        break;
                    case RegistrationResult.PasswordDoNotMatch:
                        registerViewModel.ErrorMessage = "Password does not match the confirm password";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        registerViewModel.ErrorMessage = $"Username {registerViewModel.Username} already exists";
                        break;
                    case RegistrationResult.EmailAlreadyExists:
                        registerViewModel.ErrorMessage = $"Email {registerViewModel.Email} is already in use.";
                        break;
                    default:
                        registerViewModel.ErrorMessage = "Registration failed.";
                        break;
                }
            //}
            //catch (Exception e)
            //{
            //    registerViewModel.ErrorMessage = "Registration failed.";
            //}
        }
    }
}
