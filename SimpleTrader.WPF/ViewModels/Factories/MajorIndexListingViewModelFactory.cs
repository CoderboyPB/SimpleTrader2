using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class MajorIndexListingViewModelFactory : ISimpleTraderViewModelFactory<MajorIndexListingViewModel>
    {
        private readonly IMajorIndexService majorIndexService;

        public MajorIndexListingViewModelFactory(IMajorIndexService majorIndexService)
        {
            this.majorIndexService = majorIndexService;
        }

        public MajorIndexListingViewModel CreateViewModel()
        {
            return MajorIndexListingViewModel.LoadMajorIndexViewModel(majorIndexService);
        }
    }
}
