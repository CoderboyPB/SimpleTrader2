using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class SellStockService : ISellStockService
    {
        private readonly IStockPriceService stockPriceService;
        private readonly IDataService<Account> accountService;

        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            this.stockPriceService = stockPriceService;
            this.accountService = accountService;
        }

        public async Task<Account> SellStock(Account seller, string symbol, int shares)
        {
            // Validate seller has sufficient shares
            int accountShares = GetAccountSharesForSymbol(seller, symbol);
            if(accountShares < shares)
            {
                throw new InsufficientSharesException(symbol, accountShares, shares);
            }

            double stockPrice = await stockPriceService.GetPrice(symbol);

            seller.AssetTransactions.Add(new AssetTransaction
            {
                Account = seller,
                Asset = new Asset
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                IsPurchase = false,
                Shares = shares
            });

            // update and return the updated account
            seller.Balance += stockPrice * shares;

            await accountService.Update(seller.Id, seller);

            return seller;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            return seller.AssetTransactions
                .Where(a => a.Asset.Symbol == symbol)
                .Sum(a => a.IsPurchase ? a.Shares : -a.Shares);
        }
    }
}
