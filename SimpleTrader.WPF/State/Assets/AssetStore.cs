using SimpleTrader.Domain.Models;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore accountStore;

        public AssetStore(IAccountStore accountStore)
        {
            this.accountStore = accountStore;
            accountStore.StateChanged += () => StateChanged?.Invoke();
        }

        public double AccountBalance => accountStore.CurrentAccount?.Balance ?? 0;
        public IEnumerable<AssetTransaction> AssetTransactions => accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();

        public event Action StateChanged;
    }
}
