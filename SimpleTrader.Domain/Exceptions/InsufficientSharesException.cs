using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        public string Symbol { get; }
        public int Shares { get; }
        public int RequiredShares { get; }

        public InsufficientSharesException(string symbol, int shares, int requiredShares)
        {
            Symbol = symbol;
            Shares = shares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string message, string symbol = null, int shares = 0, int requiredShares = 0) : base(message)
        {
            Symbol = symbol;
            Shares = shares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string message, Exception innerException, string symbol, int shares, int requiredShares) : base(message, innerException)
        {
            Symbol = symbol;
            Shares = shares;
            RequiredShares = requiredShares;
        }
    }
}
