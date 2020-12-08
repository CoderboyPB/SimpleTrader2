using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MajorIndexListingViewModel MajorIndexListingViewModel { get; }
        public AssetSummaryViewModel AssetSummaryViewModel { get; }

        public HomeViewModel(MajorIndexListingViewModel majorIndexViewModel, AssetSummaryViewModel assetSummaryViewModel)
        {
            MajorIndexListingViewModel = majorIndexViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }
    }
}
