using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class SellStockServiceTests 
    {
        private SellStockService sellStockService;
        private Mock<IStockPriceService> mockStockPriceService;
        private Mock<IDataService<Account>> mockAccountService;

        [SetUp]
        public void SetUp()
        {
            mockStockPriceService = new Mock<IStockPriceService>();
            mockAccountService = new Mock<IDataService<Account>>();

            sellStockService = new SellStockService(mockStockPriceService.Object, mockAccountService.Object);
        }

        [Test]
        public void SellStock_WithInvalidSymbol_ThrowsInvalidSymbolException()
        {
            string symbol = "T";
            Account seller = CreateAccount(symbol,10);

            Assert.ThrowsAsync<InsufficientSharesException>(() => sellStockService.SellStock(seller, symbol, 20));
        }

        private static Account CreateAccount(string symbol, int shares)
        {
            return new Account
            {
                AssetTransactions = new List<AssetTransaction>
                {
                    new AssetTransaction
                    {
                        Asset = new Asset
                        {
                            Symbol = symbol,
                        },
                        IsPurchase = true,
                        Shares = shares
                    }
                }
            };
        }

        [Test]
        public void SellStock_WithInsufficientShares_ThrowsInsufficientSharesException()
        {
            string expectedInvalidSymbol = "bad_symbol";
            Account seller = CreateAccount(expectedInvalidSymbol, 10);

            mockStockPriceService.Setup(s => s.GetPrice(expectedInvalidSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvalidSymbol));

            InvalidSymbolException ex = Assert.ThrowsAsync<InvalidSymbolException>(() => sellStockService.SellStock(seller, expectedInvalidSymbol, 5));
            string actualInvalidSymbol = ex.Symbol;
            Assert.AreEqual(expectedInvalidSymbol, actualInvalidSymbol);
        }

        //[Test]
        //public void SellStock_WithInsufficientShares_ThrowsInsufficientSharesException()
        //{

        //}
    }
}
