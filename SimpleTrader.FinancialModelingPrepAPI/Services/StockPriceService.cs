﻿using Newtonsoft.Json;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingPrepHttpClient client;
        public StockPriceService(FinancialModelingPrepHttpClient client)
        {
            this.client = client;
        }

        public async Task<double> GetPrice(string symbol)
        {
            string uri = $"stock/real-time-price/{symbol}";
            StockPriceResult stockPriceResult = await client.GetAsync<StockPriceResult>(uri);

            if(stockPriceResult.Price == 0)
            {
                throw new InvalidSymbolException(symbol);
            }

            return stockPriceResult.Price;    
        }
    }
}
