using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClientFactory
    {
        public readonly string apikey;

        public FinancialModelingPrepHttpClientFactory(string apikey)
        {
            this.apikey = apikey;
        }

        public FinancialModelingPrepHttpClient CreateHttpClient()
        {
            return new FinancialModelingPrepHttpClient(apikey);
        }
    }
}
