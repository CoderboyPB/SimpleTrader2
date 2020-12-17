using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        public AssetListingViewModel AssetListingViewModel { get; }

        public SellViewModel(AssetStore assetStore, IStockPriceService stockPriceService, ISellStockService sellStockService, IAccountStore accountStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

        }

        private AssetViewModel selectedAsset;
        private readonly IStockPriceService stockPriceService;

        public AssetViewModel SelectedAsset
        {
            get { return selectedAsset; }
            set 
            { 
                selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int sharesToSell;

        public int SharesToSell
        {
            get { return sharesToSell; }
            set 
            { 
                sharesToSell = value;
                OnPropertyChanged(nameof(SharesToSell));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice => SharesToSell * StockPrice;

        public string Symbol => SelectedAsset?.Symbol;

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get { return _searchResultSymbol; }
            set
            {
                _searchResultSymbol = value.ToUpper();
                OnPropertyChanged(nameof(SearchResultSymbol));
            }
        }

        private double _stockPrice;
        public double StockPrice
        {
            get { return _stockPrice; }
            set
            {
                _stockPrice = value;
                OnPropertyChanged("StockPrice");
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public MessageViewModel StatusMessageViewModel { get; }
        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }

        public ICommand SearchSymbolCommand { get;  }
        public ICommand SellStockCommand { get; }
    }
}
