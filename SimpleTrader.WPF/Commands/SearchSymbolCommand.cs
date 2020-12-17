using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly ISearchSymbolViewModel viewModel;
        private readonly IStockPriceService stockPriceService;

        public SearchSymbolCommand(ISearchSymbolViewModel viewModel, IStockPriceService stockPriceService)
        {
            this.viewModel = viewModel;
            this.stockPriceService = stockPriceService;
        }
       
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                double stockPrice = await stockPriceService.GetPrice(viewModel.Symbol);
                viewModel.StockPrice = stockPrice;
                viewModel.SearchResultSymbol = viewModel.Symbol.ToUpper();
            }
            catch (InvalidCastException)
            {
                viewModel.ErrorMessage = "Symbol does not Exist";
            }
            catch (Exception)
            {
                viewModel.ErrorMessage = "Failed to get symbol information.";
            }
        }
    }
}
