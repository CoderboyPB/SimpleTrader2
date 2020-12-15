using SimpleTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore assetStore;
        private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets;
        private readonly ObservableCollection<AssetViewModel> assets;
        public IEnumerable<AssetViewModel> Assets => assets;

        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets)
        {
            this.assetStore = assetStore;
            assets = new ObservableCollection<AssetViewModel>();

            this.filterAssets = filterAssets;

            assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        public AssetListingViewModel(AssetStore assetStore) : this(assetStore, a => a)
        {
        }

        private void AssetStore_StateChanged()
        {
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);

            assetViewModels = filterAssets(assetViewModels);

            assets.Clear();

            foreach (AssetViewModel assetViewModel in assetViewModels)
            {
                assets.Add(assetViewModel);
            }
        }
    }
}
