using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class SimpleTraderViewModelAbstractFactory : ISimpleTraderViewModelAbstractFactory
    {
        private ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory;
        private ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory;

        public SimpleTraderViewModelAbstractFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory, ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory)
        {
            this.homeViewModelFactory = homeViewModelFactory;
            this.portfolioViewModelFactory = portfolioViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return homeViewModelFactory.CreateViewModel();
                case ViewType.Portfolio:
                    return portfolioViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", "viewType");
            }
        }
    }
}
