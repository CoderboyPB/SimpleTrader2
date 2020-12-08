using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Accounts
{
    public class AccountStore : IAccountStore 
    {
        private Account currentAccount;

        public Account CurrentAccount
        {
            get { return currentAccount; }
            set 
            { 
                currentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
