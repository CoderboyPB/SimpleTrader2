using SimpleTrader.Domain.Models;
using SimpleTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore assetStore;
        private readonly ObservableCollection<AssetViewModel> topAssets;

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            this.assetStore = assetStore;
            topAssets = new ObservableCollection<AssetViewModel>();

            assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares)
                .Take(3);

            topAssets.Clear();

            foreach(AssetViewModel assetViewModel in assetViewModels)
            {
                topAssets.Add(assetViewModel);
            }
        }

        public double AccountBalance => assetStore.AccountBalance;
        public IEnumerable<AssetViewModel> TopAssets => topAssets;
    }
}
