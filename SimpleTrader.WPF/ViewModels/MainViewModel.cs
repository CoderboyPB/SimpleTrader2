using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Factories.ISimpleTraderViewModelFactory _viewModelFactory;
        private INavigator _navigator;
        private IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator, ISimpleTraderViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _viewModelFactory = viewModelFactory;

            _navigator.StateChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
            _authenticator.StateChanged += () => OnPropertyChanged(nameof(IsLoggedIn));

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
