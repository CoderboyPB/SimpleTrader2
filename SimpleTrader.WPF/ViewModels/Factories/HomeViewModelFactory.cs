using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory : ISimpleTraderViewModelFactory<HomeViewModel>
    {
        private readonly ISimpleTraderViewModelFactory<MajorIndexListingViewModel> majorIndexViewModelFactory;

        public HomeViewModelFactory(ISimpleTraderViewModelFactory<MajorIndexListingViewModel> majorIndexViewModelFactory)
        {
            this.majorIndexViewModelFactory = majorIndexViewModelFactory;
        }

        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(majorIndexViewModelFactory.CreateViewModel());
        }
    }
}
