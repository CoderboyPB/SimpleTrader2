using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static SimpleTrader.Domain.Services.AuthenticationServices.IAuthenticationService;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService accountService;
        private readonly IPasswordHasher hasher;

        public AuthenticationService(IAccountService _accountService, IPasswordHasher hasher)
        {
            accountService = _accountService;
            this.hasher = hasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await accountService.GetByUsername(username);

            PasswordVerificationResult passwordResult = hasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);
           
            if(passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if(password != confirmPassword)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }

            Account emailAccount = await accountService.GetByEmail(email);
            if(emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            Account usernameAccount = await accountService.GetByUsername(username);
            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if(result == RegistrationResult.Success)
            {
                string hashedPassword = hasher.HashPassword(password);

                User user = new User
                {
                    Email = email,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DateJoined = DateTime.Now
                };

                Account account = new Account
                {
                    AccountHolder = user
                };

                await accountService.Create(account);
            }

            return result;
        }
    }
}
