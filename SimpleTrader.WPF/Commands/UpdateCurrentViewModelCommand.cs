using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator navigator;
        private readonly ISimpleTraderViewModelAbstractFactory viewModelAbstractFactory;

        public event EventHandler CanExecuteChanged;

        public UpdateCurrentViewModelCommand(INavigator navigator, ISimpleTraderViewModelAbstractFactory viewModelAbstractFactory)
        {
            this.navigator = navigator;
            this.viewModelAbstractFactory = viewModelAbstractFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                navigator.CurrentViewModel = viewModelAbstractFactory.CreateViewModel(viewType);
            }
        }
    }
}