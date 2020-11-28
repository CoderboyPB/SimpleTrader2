using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    class SearchSymbolCommand : ICommand
    {
        private readonly BuyViewModel viewModel;
        private readonly IStockPriceService stockPriceService;

        public SearchSymbolCommand(BuyViewModel viewModel, IStockPriceService stockPriceService)
        {
            this.viewModel = viewModel;
            this.stockPriceService = stockPriceService;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                double stockPrice = await stockPriceService.GetPrice(viewModel.Symbol);
                viewModel.StockPrice = stockPrice;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
