using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SimpleTrader.Domain.Services.AuthenticationServices.IAuthenticationService;

namespace SimpleTrader.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        public Account CurrentAccount { get; }
        bool isLoggedIn { get; }

        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task<bool> Login(string username, string password);
        void Logout();
    }
}
