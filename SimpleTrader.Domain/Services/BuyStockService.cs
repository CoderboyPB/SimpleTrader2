using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService stockPriceService;
        private readonly IDataService<Account> dataService;

        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> dataService)
        {
            this.stockPriceService = stockPriceService;
            this.dataService = dataService;
        }
        public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
        {
            double stockPrice = await stockPriceService.GetPrice(symbol);

            double transactionPrice = stockPrice * shares;
            if(transactionPrice > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);
            }

            AssetTransaction transaction = new AssetTransaction
            {
                Account = buyer,
                Asset = new Asset
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = true
            };

            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            await dataService.Update(buyer.Id, buyer);

            return buyer;
        }
    }
}
