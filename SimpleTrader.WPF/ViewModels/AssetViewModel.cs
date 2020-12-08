using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetViewModel
    {
        public AssetViewModel(string symbol, int shares)
        {
            Symbol = symbol;
            Shares = shares;
        }

        public string Symbol { get; }
        public int Shares { get; }
    }
}
