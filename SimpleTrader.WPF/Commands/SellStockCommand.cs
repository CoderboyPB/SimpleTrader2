using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel sellViewModel;
        private readonly ISellStockService sellStockService;
        private readonly IAccountStore accountStore;
        public SellStockCommand(SellViewModel sellViewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            this.sellViewModel = sellViewModel;
            this.sellStockService = sellStockService;
            this.accountStore = accountStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                string symbol = sellViewModel.Symbol;
                int sharesToSell = sellViewModel.SharesToSell;

                Account account = await sellStockService.SellStock(accountStore.CurrentAccount, symbol, sharesToSell);

                accountStore.CurrentAccount = account;

                sellViewModel.SearchResultSymbol = string.Empty;
                sellViewModel.StatusMessage = $"Successfully sold {sharesToSell} of {symbol}";
            }
            catch (InsufficientSharesException ex)
            {
                sellViewModel.ErrorMessage = $"Account has insufficient shares. You only have {ex.Shares} shares.";
            }
            catch (InvalidSymbolException)
            {
                sellViewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                sellViewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
