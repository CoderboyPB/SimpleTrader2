﻿using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SimpleTrader.Domain.Services.AuthenticationServices.IAuthenticationService;

namespace SimpleTrader.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public event Action StateChanged;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            this._accountStore = accountStore;
        }

        public Account CurrentAccount
        {
            get
            {
                return _accountStore.CurrentAccount;
            }
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public async Task Login(string username, string password)
        {
            CurrentAccount = await _authenticationService.Login(username, password);
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword);
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
