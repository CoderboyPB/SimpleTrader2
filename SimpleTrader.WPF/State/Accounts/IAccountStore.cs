using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        public Account CurrentAccount { get; set; }
        public event Action StateChanged;
    }
}
