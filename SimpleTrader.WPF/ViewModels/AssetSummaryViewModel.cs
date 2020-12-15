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
        public double AccountBalance => assetStore.AccountBalance;
        public AssetListingViewModel AssetListingViewModel { get; }

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            this.assetStore = assetStore;
            AssetListingViewModel = new AssetListingViewModel(assetStore, a => a.Take(3));

            assetStore.StateChanged += AssetStore_StateChanged;
        }

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
        }
    }
}
