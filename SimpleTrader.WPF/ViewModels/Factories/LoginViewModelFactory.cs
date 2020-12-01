using SimpleTrader.WPF.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class LoginViewModelFactory : ISimpleTraderViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator authenticator;

        public LoginViewModelFactory(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(authenticator);
        }
    }
}
